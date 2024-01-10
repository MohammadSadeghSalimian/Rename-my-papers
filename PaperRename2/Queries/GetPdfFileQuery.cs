using System.IO;
using MediatR;

namespace PaperRename2.Queries
{
    public record GetPdfFileQuery():IRequest<FileInfo>;
}
