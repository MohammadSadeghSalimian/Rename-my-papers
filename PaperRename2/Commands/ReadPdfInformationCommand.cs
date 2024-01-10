using System.IO;
using MediatR;

namespace PaperRename2.Commands
{
    public record ReadPdfInformationCommand(FileInfo PdfFile) : IRequest<Unit>;
}
