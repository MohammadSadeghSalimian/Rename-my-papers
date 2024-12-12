using MediatR;
using PaperRename2.App.Services;

namespace PaperRename2.App.Commands;

public class GetPdfFilesHandler(IFolderManager folderManager) : IRequestHandler<GetPdfFilesQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetPdfFilesQuery request, CancellationToken cancellationToken)
    {
        folderManager.RootFolder = request.Root;
        var aa = folderManager.GetPdfFiles(request.Root).Select(x => x.Name);
        return await Task.FromResult(aa);
    }
}