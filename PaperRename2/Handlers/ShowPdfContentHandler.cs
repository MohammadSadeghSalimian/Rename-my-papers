using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.Commands;
using PaperRename2.Services;

namespace PaperRename2.Handlers;

public class ShowPdfContentHandler : IRequestHandler<ShowPdfFileContentCommand, Unit>
{
    private readonly IPdfManager _pdfManager;

    public ShowPdfContentHandler(IPdfManager pdfManager)
    {
        _pdfManager = pdfManager;
    }

    public async Task<Unit> Handle(ShowPdfFileContentCommand request, CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            _pdfManager.OpenPdfFile();
        }, cancellationToken);
        return Unit.Value;

    }
}