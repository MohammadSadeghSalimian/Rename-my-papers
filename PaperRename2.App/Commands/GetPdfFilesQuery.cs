using MediatR;

namespace PaperRename2.App.Commands;

public record GetPdfFilesQuery(DirectoryInfo Root) : IRequest<IEnumerable<string>>;