using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.Commands;
using PaperRename2.Services;

namespace PaperRename2.Handlers;

public class ReadPdfInformationHandler : IRequestHandler<ReadPdfInformationCommand, Unit>
{
    private readonly IPdfManager _pdfManager;
    private readonly IPaperModel _paperModel;

    public ReadPdfInformationHandler(IPdfManager pdfManager, IPaperModel paperModel)
    {
        _pdfManager = pdfManager;
        _paperModel = paperModel;
    }
    public async Task<Unit> Handle(ReadPdfInformationCommand request, CancellationToken cancellationToken)
    {
        _pdfManager.Close();
        await Task.Run(() =>
        {
            _pdfManager.LoadPdf(request.PdfFile.FullName);
        }, cancellationToken);
        _paperModel.Author = _pdfManager.GetAuthorName();
        _paperModel.Title = _pdfManager.GetTitle();
        _paperModel.Year = _pdfManager.GetYear();
        _paperModel.Normalize();
        return Unit.Value;
    }
        
}