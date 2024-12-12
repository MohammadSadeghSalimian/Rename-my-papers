namespace PaperRename2.Wpf.ViewModels;

public interface ISharedKeys
{
    public bool IsFileOpened { get; set; }
    public bool NameAvailable { get; set; }
    public bool RootFolderSelected { get; set; }
   
}