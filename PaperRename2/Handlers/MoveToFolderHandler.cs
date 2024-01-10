using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.Commands;
using PaperRename2.Services;

namespace PaperRename2.Handlers;

public class MoveToFolderHandler : IRequestHandler<MoveToFolderCommand, IReadOnlyList<string>>
{
    private readonly IFolderManager _folderManager;

    public MoveToFolderHandler(IFolderManager folderManager)
    {
        _folderManager = folderManager;
    }

    public async Task<IReadOnlyList<string>> Handle(MoveToFolderCommand request, CancellationToken cancellationToken)
    {
        if (request.Files==null || !request.Files.Any())
        {
            return Array.Empty<string>();
        }
        var root = _folderManager.RootFolder.FullName;
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