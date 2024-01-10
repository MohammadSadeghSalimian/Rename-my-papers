using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.Commands;
using PaperRename2.Services;

namespace PaperRename2.Handlers;

public class PdfWithErrorHandler : IRequestHandler<ReadInformationWithErrorCommand, Unit>
{
    private readonly IPdfManager _pdfManager;
    private readonly IMessageUnit _messageUnit;

    public PdfWithErrorHandler(IPdfManager pdfManager,IMessageUnit messageUnit)
    {
        _pdfManager = pdfManager;
        _messageUnit = messageUnit;
    }
    public async Task<Unit> Handle(ReadInformationWithErrorCommand request, CancellationToken cancellationToken)
    {
        _pdfManager.FileName = request.PdfFile;
        _pdfManager.Close();
        _messageUnit.ErrorMessage(request.Error);
        return await Unit.Task;
    }
}