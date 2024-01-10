using Splat;
using System.Reactive.Disposables;
using PaperRename2.ViewModels;
using ReactiveUI;

namespace PaperRename2.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : BaseWindow<MainViewModel>
    {
       
        public MainWindow()
        {
            this.ViewModel = Locator.Current.GetService<MainViewModel>();
            
            InitializeComponent();

            this.Setup();

        }

        #region Overrides of BaseWindow<MainViewModel>

        protected override void SetupCommands(CompositeDisposable d)
        {
            
        }
        protected override void SetupElements(CompositeDisposable d)
        {
            this.OneWayBind(ViewModel, x => x.EditVm, v => v.EditUc.ViewModel).DisposeWith(d);
            this.OneWayBind(ViewModel, x => x.FileListVm, v => v.FileListUc.ViewModel).DisposeWith(d);
        }

        #endregion

      
    }
}
