using MediatR;
using PaperRename2.Core;

namespace PaperRename2.App.Commands
{
    public record ReadPdfInformationCommand(FileInfo PdfFile) : IRequest<IPaperModel>;
}
