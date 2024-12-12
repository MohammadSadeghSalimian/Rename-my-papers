using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PaperRename2.Wpf.ViewModels;

public class SharedKeys : ReactiveObject, ISharedKeys
{
    [Reactive] public bool IsFileOpened { get; set; }
    [Reactive] public bool NameAvailable { get; set; }
    [Reactive] public bool RootFolderSelected { get; set; }
    
}