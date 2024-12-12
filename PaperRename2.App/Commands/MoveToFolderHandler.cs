using MediatR;
using PaperRename2.App.Services;

namespace PaperRename2.App.Commands;

public class MoveToFolderHandler(IFolderManager folderManager)
    : IRequestHandler<MoveToFolderCommand, IReadOnlyList<string>>
{
    public async Task<IReadOnlyList<string>> Handle(MoveToFolderCommand request, CancellationToken cancellationToken)
    {
        if (request.Files == null || !request.Files.Any())
        {
            return Array.Empty<string>();
        }
        var root = folderManager.RootFolder.FullName;
        var l = new List<string>();

        foreach (var file in request.Files)
        {
            var p1 = new FileInfo(Path.Combine(root, file));
            var p2 = new FileInfo(Path.Combine(root, request.FolderName, file));
            try
            {
                if (p2.Directory is { Exists: false })
                {
                    p2.Directory.Create();
                }
                p1.MoveTo(p2.FullName);

            }
            catch (Exception e)
            {
                l.Add(file);
            }
        }

        return await Task.FromResult(l);
    }
}