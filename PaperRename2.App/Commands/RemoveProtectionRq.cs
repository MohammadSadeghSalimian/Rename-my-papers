using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PaperRename2.App.Services;

namespace PaperRename2.App.Commands
{
    public record RemoveProtectionRq(FileInfo File) : IRequest<bool>;

    public class RemoveProtectionHandler(IProtectionRemover protectionRemover)
        : IRequestHandler<RemoveProtectionRq, bool>
    {
        public async Task<bool> Handle(RemoveProtectionRq request, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                protectionRemover.Remove(request.File);
            }, cancellationToken);
            return true;
        }
    }

}
