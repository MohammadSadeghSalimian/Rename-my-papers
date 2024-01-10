namespace PaperRename2.ViewModels;

public interface ISharedModel
{
    public IKeyContainer KeyContainer { get; }
    public IEventContainer EventContainer { get; }
    
}