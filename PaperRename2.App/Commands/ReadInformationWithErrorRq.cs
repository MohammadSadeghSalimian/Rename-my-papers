using MediatR;
using PaperRename2.Core;

namespace PaperRename2.App.Commands;

public record ReadInformationWithErrorRq(FileInfo PdfFile, Exception Error) : IRequest<IPaperModel>;