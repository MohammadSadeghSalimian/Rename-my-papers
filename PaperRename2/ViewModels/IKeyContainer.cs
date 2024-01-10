using System;

namespace PaperRename2.ViewModels;

public interface IKeyContainer
{
    public bool IsFileOpened { get; set; }
    public bool NameAvailable { get; set; }
    public bool RootFolderSelected { get; set; }
   
}