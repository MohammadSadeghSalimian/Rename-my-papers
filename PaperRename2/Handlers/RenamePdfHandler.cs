using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.Commands;
using PaperRename2.Services;
using PaperRename2.ViewModels;

namespace PaperRename2.Handlers;

public class RenamePdfHandler : IRequestHandler<RenamePdfCommand, Unit>
{
    private readonly IPdfManager _pdfManager;
    private readonly IMessageUnit _messageUnit;
    private readonly IEventContainer _eventContainer;

    public RenamePdfHandler(IPdfManager pdfManager, IMessageUnit messageUnit,IEventContainer eventContainer)
    {
        _pdfManager = pdfManager;
        _messageUnit = messageUnit;
        _eventContainer = eventContainer;
    }
    public async  Task<Unit> Handle(RenamePdfCommand request, CancellationToken cancellationToken)
    {
        _pdfManager.Close();
        _pdfManager.Rename(request.Name);
        _eventContainer.FileRenamed(_pdfManager.FileName.Name,request.Name);
        _messageUnit.InformationMessage("The file is renamed!");
        return await Task.FromResult(Unit.Value);
    }
}