using System.IO;
using MediatR;

namespace PaperRename2.Queries;

public record GetFolderQuery() : IRequest<DirectoryInfo>;