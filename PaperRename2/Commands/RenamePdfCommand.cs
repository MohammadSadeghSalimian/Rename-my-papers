using MediatR;

namespace PaperRename2.Commands;

public record RenamePdfCommand(string Name) : IRequest<Unit>;