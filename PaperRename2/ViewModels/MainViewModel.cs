using MediatR;
using PaperRename2.Services;

namespace PaperRename2.ViewModels
{
    public sealed class MainViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;
        private readonly IKeyContainer _keyContainer;
        private readonly IMessageUnit _messageUnit;

        public MainViewModel(IMessageUnit messageUnit, IKeyContainer keyContainer, IMediator mediator, EditVm editVm, FileListVm fileListVm)
        {


            _messageUnit = messageUnit;
            _keyContainer = keyContainer;
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
