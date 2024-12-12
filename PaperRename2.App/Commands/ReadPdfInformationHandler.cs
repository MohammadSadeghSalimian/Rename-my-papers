using MediatR;
using PaperRename2.App.Services;
using PaperRename2.Core;

namespace PaperRename2.App.Commands;

public class ReadPdfInformationHandler(IPdfManager pdfManager, IPaperModel paperModel)
    : IRequestHandler<ReadPdfInformationCommand, IPaperModel>
{
    public async Task<IPaperModel> Handle(ReadPdfInformationCommand request, CancellationToken cancellationToken)
    {
        pdfManager.Close();
        await Task.Run(() =>
        {
            pdfManager.LoadPdf(request.PdfFile.FullName);
        }, cancellationToken);
        paperModel.Author = pdfManager.GetAuthorName();
        paperModel.Title = pdfManager.GetTitle();
        paperModel.Year = pdfManager.GetYear();
        paperModel.Normalize();
        return paperModel;
    }

}