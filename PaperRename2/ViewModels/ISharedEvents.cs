using System;
using PaperRename2.Wpf.Models;

namespace PaperRename2.Wpf.ViewModels;

public interface ISharedEvents
{
    IObservable<FileRenamedModel> FileRenamedEvent { get; }
    void FileRenamed(string previousName,string newName);
}