using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Controls;
using PaperRename2.ViewModels;
using ReactiveUI;

namespace PaperRename2.Views.Ucs
{
    /// <summary>
    /// Interaction logic for FileListUc.xaml
    /// </summary>
    public sealed partial class FileListUc : RBaseUc<FileListVm>
    {
        public FileListUc()
        {
            InitializeComponent();
            Setup();
        }

        protected override void SetupElements(CompositeDisposable d)
        {
            this.OneWayBind(ViewModel, x => x.Files, v => v.ListBox.ItemsSource).DisposeWith(d);

            this.Bind(ViewModel, x => x.SelectedPdf, v => v.ListBox.SelectedItem).DisposeWith(d);
        }

        protected override void SetupCommands(CompositeDisposable d)
        {
            this.BindCommand(ViewModel, x => x.LoadFolderCmd, v => v.RootFolderBtn).DisposeWith(d);
            this.BindCommand(ViewModel, x => x.LoadPdfFileCmd, v => v.LoadPdfBtn).DisposeWith(d);
            this.BindCommand(ViewModel, x => x.MoveToLaterCmd, v => v.LaterBtn).DisposeWith(d);
            this.BindCommand(ViewModel, x => x.MoveToRenamedCmd, v => v.RenamedBtn).DisposeWith(d);
            this.BindCommand(ViewModel, x => x.OpenPdfCmd, v => v.OpenPdfBtn).DisposeWith(d);
          
        }
    }
}
