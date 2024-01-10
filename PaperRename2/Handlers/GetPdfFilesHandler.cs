using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.Queries;
using PaperRename2.Services;

namespace PaperRename2.Handlers;

public class GetPdfFilesHandler : IRequestHandler<GetPdfFilesQuery,IEnumerable<string>>
{
    private readonly IFolderManager _folderManager;

    public GetPdfFilesHandler(IFolderManager folderManager)
    {
        _folderManager = folderManager;
    }

    public async Task<IEnumerable<string>> Handle(GetPdfFilesQuery request, CancellationToken cancellationToken)
    {
        _folderManager.RootFolder = request.Root;
        var aa = _folderManager.GetPdfFiles(request.Root).Select(x => x.Name);
        return await Task.FromResult(aa);
    }
}