using MediatR;

namespace PaperRename2.App.Commands;

public record MoveToFolderCommand(string FolderName, IEnumerable<string> Files) : IRequest<IReadOnlyList<string>>;