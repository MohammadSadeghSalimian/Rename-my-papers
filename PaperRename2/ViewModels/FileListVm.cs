using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using MediatR;
using PaperRename2.Commands;
using PaperRename2.Models;
using PaperRename2.Queries;
using PaperRename2.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Unit = System.Reactive.Unit;

namespace PaperRename2.ViewModels;

public sealed class FileListVm : BaseViewModel
{
    private readonly ISharedModel _sharedModel;
    private readonly IFolderManager _folderManager;
    private readonly IMessageUnit _messageUnit;
    private readonly IMediator _mediator;
    private readonly SourceList<PdfNameModel> _fileSource;
    private List<IDisposable> _disposables;
    public FileListVm(IMediator mediator, IMessageUnit messageUnit, ISharedModel sharedModel, IFolderManager folderManager)
    {
        _disposables=new List<IDisposable>();
        _mediator = mediator;
        _messageUnit = messageUnit;
        _sharedModel = sharedModel;
        _folderManager = folderManager;
        _fileSource = new SourceList<PdfNameModel>();
        _fileSource.Connect().Bind(out _files).Subscribe();
        var d=_sharedModel.EventContainer.FileRenamedEvent.Subscribe(Refresh);
        _disposables.Add(d);
        SetupCmd();
    }

    private readonly ReadOnlyObservableCollection<PdfNameModel> _files;
    public ReadOnlyObservableCollection<PdfNameModel> Files => _files;



    public ReactiveCommand<Unit, Unit> LoadFolderCmd { get; private set; }
    public ReactiveCommand<Unit, Unit> LoadPdfFileCmd { get; private set; }
    public ReactiveCommand<Unit, Unit> MoveToRenamedCmd { get; private set; }
    public ReactiveCommand<Unit, Unit> MoveToLaterCmd { get; private set; }
    public ReactiveCommand<Unit, Unit> OpenPdfCmd { get; private set; }

    [Reactive] public PdfNameModel SelectedPdf { get; set; }

    protected override void SetupCmd()
    {
        var can = this.WhenAnyValue(x => x._sharedModel.KeyContainer.RootFolderSelected);
        var can2 = this.WhenAnyValue(x => x._sharedModel.KeyContainer.IsFileOpened);
        LoadFolderCmd = ReactiveCommand.CreateFromTask(SelectRootFolder);
        LoadPdfFileCmd = ReactiveCommand.CreateFromTask(LoadSelectedPdf, can);
        MoveToLaterCmd = ReactiveCommand.CreateFromTask(MoveToLater, can);
        MoveToRenamedCmd = ReactiveCommand.CreateFromTask(MoveToRenamed, can);
        OpenPdfCmd = ReactiveCommand.CreateFromTask(OpenPdfFile, can2);
    }

    private async Task SelectRootFolder()
    {
        var folder = await _mediator.Send(new GetFolderQuery());
        if (folder is not { Exists: true })
        {

            return;
        }
        _fileSource.Clear();
        var ll = await _mediator.Send(new GetPdfFilesQuery(folder));
        _fileSource.AddRange(ll.Select(x => new PdfNameModel(x)));
        this._sharedModel.KeyContainer.RootFolderSelected = true;
    }

    private async Task LoadSelectedPdf()
    {
        if (string.IsNullOrEmpty(this.SelectedPdf?.Name))
        {
            return;
        }
        var f = _folderManager.GetFileByName(SelectedPdf.Name);
        if (f == null)
        {
            return;
        }

        try
        {
            await _mediator.Send(new ReadPdfInformationCommand(f));
            _sharedModel.KeyContainer.IsFileOpened = true;
        }
        catch (Exception e)
        {

            await _mediator.Send(new ReadInformationWithErrorCommand(f, e));
            _sharedModel.KeyContainer.IsFileOpened = true;
        }
    }

    private async Task MoveToLater()
    {
        var notCompleted = await _mediator.Send(new MoveToFolderCommand("Later", Files.Where(x => x.IsSelected).Select(x => x.Name)));
        if (notCompleted.Any())
        {
            _messageUnit.ErrorMessage($"The following files cannot be moved!\r\n {string.Join(Environment.NewLine,notCompleted)}");
        }
        _fileSource.RemoveMany(Files.Where(x=>x.IsSelected));
    }
    private async Task MoveToRenamed()
    {
        var notCompleted = await _mediator.Send(new MoveToFolderCommand("Renamed", Files.Where(x => x.IsSelected).Select(x => x.Name)));
        if (notCompleted.Any())
        {
            _messageUnit.ErrorMessage($"The following files cannot be moved!\r\n {string.Join(Environment.NewLine, notCompleted)}");
        }
        _fileSource.RemoveMany(Files.Where(x => x.IsSelected));
    }

    private void Refresh(FileRenamedModel model)
    {
        var a = Files?.FirstOrDefault(x => x.Name == model.PreviousName);
        if (a == null)
        {
            return;
        }
        _fileSource.Replace(a,new PdfNameModel(model.NewName)
        {
            IsSelected = true,
        });
    }
    private async Task OpenPdfFile()
    {
        try
        {
            await _mediator.Send(new ShowPdfFileContentCommand());
        }
        catch (Exception e)
        {
            _messageUnit.ErrorMessage(e);
        }
    }
}