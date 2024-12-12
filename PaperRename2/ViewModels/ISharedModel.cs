namespace PaperRename2.Wpf.ViewModels;

public interface ISharedModel
{
    public ISharedKeys SharedKeys { get; }
    public ISharedEvents SharedEvents { get; }
    
}