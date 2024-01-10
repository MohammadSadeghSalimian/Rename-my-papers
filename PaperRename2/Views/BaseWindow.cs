using ReactiveUI;
using Splat;
using System.Reactive.Disposables;
using MahApps.Metro.Controls;

namespace PaperRename2.Views
{
    public abstract class BaseWindow<T> :MetroWindow,  IViewFor<T> where T : class
    {
        protected T ViewModelP;
        object IViewFor.ViewModel
        {
            get => ViewModelP;
            set => ViewModel = (T)value;
        }
        public T ViewModel
        {
            get => ViewModelP;
            set
            {
                ViewModelP = value;
                DataContext = ViewModelP;
            }
        }
        protected virtual void Setup()
        {
            this.WhenActivated(d =>
            {
                SetupElements(d);
                SetupCommands(d);
            });
        }
        protected abstract void SetupCommands(CompositeDisposable d);
        protected abstract void SetupElements(CompositeDisposable d);
        protected void GetViewModel()
        {
            ViewModel = Locator.Current.GetService<T>();
        }
    }
}
