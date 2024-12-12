using System.Reactive.Disposables;
using ReactiveUI;

namespace PaperRename2.Wpf.Views.Ucs;

public abstract class RBaseUc<T> : ReactiveUserControl<T> where T : ReactiveObject
{
    protected virtual void Setup()
    {
        this.WhenActivated(d =>
        {
            SetupElements(d);
            SetupCommands(d);
        });
    }

    protected abstract void SetupElements(CompositeDisposable d);
    protected abstract void SetupCommands(CompositeDisposable d);
}