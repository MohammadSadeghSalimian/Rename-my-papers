using MediatR;
using PaperRename2.App.Services;

namespace PaperRename2.App.Commands;

public class RenamePdfHandler(IPdfManager pdfManager)
    : IRequestHandler<RenamePdfCommand, string>
{

    public async Task<string> Handle(RenamePdfCommand request, CancellationToken cancellationToken)
    {
        pdfManager.Close();
        pdfManager.Rename(request.Name);
       
        return await Task.FromResult(pdfManager.FileName.Name);
    }
}