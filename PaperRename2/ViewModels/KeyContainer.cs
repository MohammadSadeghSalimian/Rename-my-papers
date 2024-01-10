using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PaperRename2.ViewModels;

public class KeyContainer : ReactiveObject, IKeyContainer
{
    [Reactive] public bool IsFileOpened { get; set; }
    [Reactive] public bool NameAvailable { get; set; }
    [Reactive] public bool RootFolderSelected { get; set; }
    
}