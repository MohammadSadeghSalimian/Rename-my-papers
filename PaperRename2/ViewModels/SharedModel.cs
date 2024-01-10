namespace PaperRename2.ViewModels;

public class SharedModel:ISharedModel
{
    public SharedModel(IKeyContainer keyContainer, IEventContainer eventContainer)
    {
        KeyContainer = keyContainer;
        EventContainer = eventContainer;
    }
    public IKeyContainer KeyContainer { get; }
    public IEventContainer EventContainer { get; }
}