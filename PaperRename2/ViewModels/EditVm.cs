using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.App.Commands;
using PaperRename2.App.Services;
using PaperRename2.Core;
using PaperRename2.Wpf.Services;
using ReactiveUI;
using Unit = System.Reactive.Unit;

namespace PaperRename2.Wpf.ViewModels;

public class EditVm : BaseViewModel
{
    private readonly IMediator _mediator;
    private readonly ISharedModel _sharedModel;
    private readonly IMessageUnit _messageUnit;
    private readonly ICommonDialogBuilder _dialogBuilder;
    private readonly ISharedEvents _sharedEvents;
    public IPaperModel Paper { get; }
    public EditVm(IMediator mediator, ISharedModel sharedModel, IMessageUnit messageUnit, IPaperModel paperModel,ICommonDialogBuilder dialogBuilder,ISharedEvents sharedEvents)
    {
        _mediator = mediator;
        _sharedModel = sharedModel;
        _messageUnit = messageUnit;
        _dialogBuilder = dialogBuilder;
        _sharedEvents = sharedEvents;
        Paper = paperModel;
        SetupCmd();
    }
    public ReactiveCommand<Unit, Unit> LoadFileCmd { get; private set; }
    public ReactiveCommand<Unit, Unit> GetNameCmd { get; private set; }
    public ReactiveCommand<Unit, Unit> RenameCmd { get; private set; }
    public ReactiveCommand<Unit, Unit> AddEtAlNameCmd { get; private set; }
    public ReactiveCommand<Unit, Unit> OpenFileCmd { get; private set; }
    protected sealed override void SetupCmd()
    {
        LoadFileCmd = ReactiveCommand.CreateFromTask(Load);
        var can = this.WhenAnyValue(x => x._sharedModel.SharedKeys.IsFileOpened);
        var can2 = this.WhenAnyValue(x => x._sharedModel.SharedKeys.NameAvailable);
        RenameCmd = ReactiveCommand.CreateFromTask(Rename, can2);
        GetNameCmd = ReactiveCommand.Create(GetName, can);
        AddEtAlNameCmd = ReactiveCommand.Create(AddEtAl, can);
        OpenFileCmd = ReactiveCommand.CreateFromTask(OpenPdfFile, can);

    }
    private async Task Load()
    {
        var op = _dialogBuilder.GetDialog();
        op.SetFilters("Pdf files|.pdf","epub files |.epub");
        op.DefaultExtension = "pdf";
        op.Title = "Select the paper";
        if (!op.OpenFileDialog(out var name))
        {
            return;
        }
        try
        {
            await _mediator.Send(new ReadPdfInformationCommand(new FileInfo(name)));
        }
        catch (Exception e)
        {
            await _mediator.Send(new ReadInformationWithErrorRq(new FileInfo(name), e));
        }
        _sharedModel.SharedKeys.IsFileOpened = true;
    }

    private void AddEtAl()
    {
        try
        {
            Paper.AddEtAl();

        }
        catch (Exception e)
        {
            _messageUnit.ErrorMessage(e);
        }

    }

    private void GetName()
    {
        try
        {
            Paper.GetFileName();
            _sharedModel.SharedKeys.NameAvailable = true;
        }
        catch (Exception e)
        {
            _messageUnit.ErrorMessage(e);
        }
    }
    private async Task Rename()
    {
        try
        {
           var fileName= await _mediator.Send(new RenamePdfCommand(Paper.Name));
            _sharedModel.SharedKeys.IsFileOpened = false;
            _sharedModel.SharedKeys.NameAvailable = false;

           
            _sharedEvents.FileRenamed(fileName, Paper.Name);
           await _messageUnit.InformationMessage("The file is renamed!");


        }
        catch (Exception e)
        {
           await _messageUnit.ErrorMessage(e);
        }
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