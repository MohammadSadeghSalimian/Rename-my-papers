using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.Queries;
using PaperRename2.Services;

namespace PaperRename2.Handlers;

public class OpenFolderHandler : IRequestHandler<GetFolderQuery, DirectoryInfo>
{
    private readonly ICommonDialogBuilder _dialogBuilder;

    public OpenFolderHandler(ICommonDialogBuilder dialogBuilder)
    {
        _dialogBuilder = dialogBuilder;
    }
    public async Task<DirectoryInfo> Handle(GetFolderQuery request, CancellationToken cancellationToken)
    {
        var op = _dialogBuilder.GetDialog();


        op.Title = "Select the working folder";
        return !op.OpenFolderDialog(out var name) ? null : await Task.FromResult(new DirectoryInfo(name));
    }
}