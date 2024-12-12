using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using MediatR;
using PaperRename2.App.Commands;
using PaperRename2.App.Services;
using PaperRename2.Wpf.Models;
using PaperRename2.Wpf.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Unit = System.Reactive.Unit;

namespace PaperRename2.Wpf.ViewModels;

public sealed class FileListVm : BaseViewModel
{
    private readonly ISharedModel _sharedModel;
    private readonly IFolderManager _folderManager;
    private readonly ICommonDialogBuilder _dialogBuilder;
    private readonly IMessageUnit _messageUnit;
    private readonly IMediator _mediator;
    private readonly SourceList<PdfNameModel> _fileSource;
    private readonly CompositeDisposable _disposables;
    public FileListVm(IMediator mediator, IMessageUnit messageUnit, ISharedModel sharedModel, IFolderManager folderManager,ICommonDialogBuilder dialogBuilder)
    {
        _disposables = [];
        _mediator = mediator;
        _messageUnit = messageUnit;
        _sharedModel = sharedModel;
        _folderManager = folderManager;
        _dialogBuilder = dialogBuilder;
        _fileSource = new SourceList<PdfNameModel>();
        _fileSource.Connect().Bind(out _files).Subscribe();
        var d = _sharedModel.SharedEvents.FileRenamedEvent.Subscribe(Refresh);
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

    [Reactive] public PdfNameModel SelectedFile { get; set; }
    public ReactiveCommand<Unit, Unit> RemoveProtectionCmd { get;private set; }

    protected override void SetupCmd()
    {
        var can1 = this.WhenAnyValue(x => x._sharedModel.SharedKeys.RootFolderSelected);
        var can2 = this.WhenAnyValue(x => x._sharedModel.SharedKeys.IsFileOpened);
        var can3 = this.WhenAnyValue(x => x.SelectedFile).WhereNotNull().Select(x => !string.IsNullOrEmpty(x.Name));
        var can4 = can1.CombineLatest(can3, (a, b) => a & b);
     
        LoadFolderCmd = ReactiveCommand.CreateFromTask(SelectRootFolder);
        LoadPdfFileCmd = ReactiveCommand.CreateFromTask(LoadSelectedPdf, can4);
        MoveToLaterCmd = ReactiveCommand.CreateFromTask(MoveToLater, can4);
        MoveToRenamedCmd = ReactiveCommand.CreateFromTask(MoveToRenamed, can4);
        RemoveProtectionCmd = ReactiveCommand.CreateFromTask(RemoveProtection, can4);
        OpenPdfCmd = ReactiveCommand.CreateFromTask(OpenPdfFile, can2);
    }

    private async Task RemoveProtection()
    {
        try
        {
            var f = _folderManager.GetFileByName(SelectedFile.Name);
            await _mediator.Send(new RemoveProtectionRq(f));
        }
        catch (Exception e)
        {
          await _messageUnit.ErrorMessage(e);
        }
    }

    private async Task SelectRootFolder()
    {
        var op = _dialogBuilder.GetDialog();
        op.Title = "Select the working folder";
        if (!op.OpenFolderDialog(out var name))
        {
            return;
        }
        _fileSource.Clear();
        var ll = await _mediator.Send(new GetPdfFilesQuery(new DirectoryInfo(name)));
        _fileSource.AddRange(ll.Select(x => new PdfNameModel(x)));
        this._sharedModel.SharedKeys.RootFolderSelected = true;
    }

    private async Task LoadSelectedPdf()
    {
        if (string.IsNullOrEmpty(this.SelectedFile?.Name))
        {
            return;
        }
        var f = _folderManager.GetFileByName(SelectedFile.Name);
        if (f == null)
        {
            return;
        }

        try
        {
            await _mediator.Send(new ReadPdfInformationCommand(f));
            _sharedModel.SharedKeys.IsFileOpened = true;
        }
        catch (Exception e)
        {

            await _mediator.Send(new ReadInformationWithErrorRq(f, e));
            _sharedModel.SharedKeys.IsFileOpened = true;
        }
    }

    private async Task MoveToLater()
    {
        var notCompleted = await _mediator.Send(new MoveToFolderCommand("Later", Files.Where(x => x.IsSelected).Select(x => x.Name)));
        if (notCompleted.Any())
        {
           await _messageUnit.ErrorMessage($"The following files cannot be moved!\r\n {string.Join(Environment.NewLine, notCompleted)}");
        }
        _fileSource.RemoveMany(Files.Where(x => x.IsSelected));
    }
    private async Task MoveToRenamed()
    {
        var notCompleted = await _mediator.Send(new MoveToFolderCommand("Renamed", Files.Where(x => x.IsSelected).Select(x => x.Name)));
        if (notCompleted.Any())
        {
            await _messageUnit.ErrorMessage($"The following files cannot be moved!\r\n {string.Join(Environment.NewLine, notCompleted)}");
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
        _fileSource.Replace(a, new PdfNameModel(model.NewName)
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
            await _messageUnit.ErrorMessage(e);
        }
    }
}