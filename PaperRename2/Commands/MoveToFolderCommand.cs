using System.Collections.Generic;
using MediatR;

namespace PaperRename2.Commands;

public record MoveToFolderCommand(string FolderName,IEnumerable<string> Files):IRequest<IReadOnlyList<string>>;