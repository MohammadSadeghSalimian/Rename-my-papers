using MediatR;
using PaperRename2.Wpf.Services;

namespace PaperRename2.Wpf.ViewModels
{
    public sealed class MainViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
        private readonly ISharedKeys _sharedKeys;
        private readonly IMessageUnit _messageUnit;

        public MainViewModel(IMessageUnit messageUnit, ISharedKeys sharedKeys, IMediator mediator, EditVm editVm, FileListVm fileListVm)
        {


            _messageUnit = messageUnit;
            _sharedKeys = sharedKeys;
            _mediator = mediator;
            EditVm = editVm;
            FileListVm = fileListVm;

            _messageUnit.SetParentObject(this);
            SetupCmd();
            SetupProperties();
        }

        public EditVm EditVm { get; private set; }
        public FileListVm FileListVm { get; private set; }








    }
}
