using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.Queries;
using PaperRename2.Services;

namespace PaperRename2.Handlers
{
    public class GetPdfFileHandler : IRequestHandler<GetPdfFileQuery, FileInfo>
    {
        private readonly ICommonDialogBuilder _dialogBuilder;

        public GetPdfFileHandler(ICommonDialogBuilder dialogBuilder)
        {
            _dialogBuilder = dialogBuilder;

        }
        public async Task<FileInfo> Handle(GetPdfFileQuery request, CancellationToken cancellationToken)
        {
            var op = _dialogBuilder.GetDialog();
            op.SetFilters("Pdf files|.pdf");
            op.DefaultExtension = "pdf";
            op.Title = "Select the paper";
            return !op.OpenFileDialog(out var name) ? null : await Task.FromResult(new FileInfo(name));
        }
    }
}
