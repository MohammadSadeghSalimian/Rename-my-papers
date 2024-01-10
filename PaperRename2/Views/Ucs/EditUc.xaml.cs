using PaperRename2.Services;
using Splat;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Input;
using PaperRename2.ViewModels;
using ReactiveUI;

namespace PaperRename2.Views.Ucs
{
    /// <summary>
    /// Interaction logic for EditUc.xaml
    /// </summary>
    public sealed partial class EditUc : RBaseUc<EditVm>
    {
        private readonly ITextController _textController;
        public EditUc()
        {
            InitializeComponent();
            _textController = Locator.Current.GetService<ITextController>();
            Setup();
        }
        private void ConvertBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _textController.TittleSimplify(_lastFocusedTextBox ?? sender);
        }
        private void ShortBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _textController.MakeItShort(_lastFocusedTextBox ?? sender);
        }
        private void TitleBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _textController.MakeItTitle(_lastFocusedTextBox ?? sender);
        }
        private void CapitalBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _textController.MakeUpper(_lastFocusedTextBox ?? sender);
        }
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                ShortBtn_OnClick(sender, null);
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.T)
            {
                TitleBtn_OnClick(sender, null);
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.U)
            {
                CapitalBtn_OnClick(sender, null);
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.L)
            {
                LowerBtn_OnClick(sender, null);
            }

        }

        private object _lastFocusedTextBox;
        private void Txt_OnGotFocus(object sender, RoutedEventArgs e)
        {
            _lastFocusedTextBox = sender;
        }

        private void LowerBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _textController.MakeLower(_lastFocusedTextBox ?? sender);
        }

        protected override void SetupCommands(CompositeDisposable d)
        {
            this.BindCommand(ViewModel!, v => v.LoadFileCmd, x => x.LoadBtn).DisposeWith(d);
            this.BindCommand(ViewModel!, v => v.GetNameCmd, x => x.GetNameBtn).DisposeWith(d);
            this.BindCommand(ViewModel!, v => v.RenameCmd, x => x.RenameBtn).DisposeWith(d);
            this.BindCommand(ViewModel!, v => v.AddEtAlNameCmd, x => x.EtAlBtn).DisposeWith(d);
            this.BindCommand(ViewModel!, v => v.OpenFileCmd, x => x.OpenBtn).DisposeWith(d);
            this.BindCommand(ViewModel!, v => v.AddEtAlNameCmd, x => x.InputBindings[0].Command).DisposeWith(d);
            this.BindCommand(ViewModel!, v => v.OpenFileCmd, x => x.InputBindings[1].Command).DisposeWith(d);
        }
        protected override void SetupElements(CompositeDisposable d)
        {
            this.Bind(ViewModel!, v => v.Paper.Title, x => x.TitleTxt.Text).DisposeWith(d);
            this.Bind(ViewModel!, v => v.Paper.Name, x => x.NameTxt.Text).DisposeWith(d);
            this.Bind(ViewModel!, v => v.Paper.Author, x => x.AuthorTxt.Text).DisposeWith(d);
            this.Bind(ViewModel!, v => v.Paper.Year, x => x.YearNum.Value, x => x, x =>
            {
                if (x == null)
                {
                    return 0;
                }

                return (int)x;
            }).DisposeWith(d);
        }

    }
}
