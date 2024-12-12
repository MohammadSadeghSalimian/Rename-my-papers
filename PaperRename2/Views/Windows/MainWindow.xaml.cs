using System.Reactive.Disposables;
using PaperRename2.Wpf.ViewModels;
using ReactiveUI;
using Splat;

namespace PaperRename2.Wpf.Views.Windows
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
