using System;
using System.IO;
using MediatR;

namespace PaperRename2.Commands;

public record ReadInformationWithErrorCommand(FileInfo PdfFile,Exception Error) : IRequest<Unit>;

