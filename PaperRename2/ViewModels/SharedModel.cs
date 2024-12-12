namespace PaperRename2.Wpf.ViewModels;

public class SharedModel(ISharedKeys sharedKeys, ISharedEvents sharedEvents) : ISharedModel
{
    public ISharedKeys SharedKeys { get; } = sharedKeys;
    public ISharedEvents SharedEvents { get; } = sharedEvents;
}