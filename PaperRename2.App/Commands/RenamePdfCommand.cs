using MediatR;

namespace PaperRename2.App.Commands;

public record RenamePdfCommand(string Name) : IRequest<string>;