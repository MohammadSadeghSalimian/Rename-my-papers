using ReactiveUI;

namespace PaperRename2.Wpf.ViewModels;

public abstract class BaseViewModel:ReactiveObject
{
    protected virtual void SetupCmd()
    {

    }
    protected virtual void SetupProperties()
    {
    }
}