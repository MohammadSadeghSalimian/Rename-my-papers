using MediatR;
using PaperRename2.App.Services;

namespace PaperRename2.App.Commands;

public class ShowPdfContentHandler(IPdfManager pdfManager) : IRequestHandler<ShowPdfFileContentCommand, Unit>
{
    public async Task<Unit> Handle(ShowPdfFileContentCommand request, CancellationToken cancellationToken)
    {
        await Task.Run(pdfManager.OpenPdfFile, cancellationToken);
        return Unit.Value;

    }
}