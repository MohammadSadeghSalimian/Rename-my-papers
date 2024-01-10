using System.Collections.Generic;
using System.IO;
using MediatR;

namespace PaperRename2.Queries;

public record GetPdfFilesQuery(DirectoryInfo Root) : IRequest<IEnumerable<string>>;