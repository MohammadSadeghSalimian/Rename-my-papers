using MediatR;

namespace PaperRename2.Commands;

public record ShowPdfFileContentCommand() : IRequest<Unit>;