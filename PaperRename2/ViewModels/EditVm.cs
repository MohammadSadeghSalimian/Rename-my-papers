using System;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.Commands;
using PaperRename2.Queries;
using PaperRename2.Services;
using ReactiveUI;
using Unit = System.Reactive.Unit;

namespace PaperRename2.ViewModels;

public class EditVm : BaseViewModel
{
    private readonly IMediator _mediator;
    private readonly ISharedModel _sharedModel;
    private readonly IMessageUnit _messageUnit;
    public IPaperModel Paper { get; }
    public EditVm(IMediator mediator, ISharedModel sharedModel, IMessageUnit messageUnit, IPaperModel paperModel)
    {
        _mediator = mediator;
        _sharedModel = sharedModel;
        _messageUnit = messageUnit;
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
        var can = this.WhenAnyValue(x => x._sharedModel.KeyContainer.IsFileOpened);
        var can2 = this.WhenAnyValue(x => x._sharedModel.KeyContainer.NameAvailable);
        RenameCmd = ReactiveCommand.CreateFromTask(Rename, can2);
        GetNameCmd = ReactiveCommand.Create(GetName, can);
        AddEtAlNameCmd = ReactiveCommand.Create(AddEtAl, can);
        OpenFileCmd = ReactiveCommand.CreateFromTask(OpenPdfFile, can);

    }
    private async Task Load()
    {
        var f = await _mediator.Send(new GetPdfFileQuery());
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
            _sharedModel.KeyContainer.NameAvailable = true;
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
            await _mediator.Send(new RenamePdfCommand(Paper.Name));
            _sharedModel.KeyContainer.IsFileOpened = false;
            _sharedModel.KeyContainer.NameAvailable = false;
        }
        catch (Exception e)
        {
            _messageUnit.ErrorMessage(e);
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
            _messageUnit.ErrorMessage(e);
        }
    }
}