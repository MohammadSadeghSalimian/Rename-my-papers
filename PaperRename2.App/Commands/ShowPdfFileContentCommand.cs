using MediatR;

namespace PaperRename2.App.Commands;

public record ShowPdfFileContentCommand() : IRequest<Unit>;