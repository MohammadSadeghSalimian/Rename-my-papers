using System;
using PaperRename2.Models;

namespace PaperRename2.ViewModels;

public interface IEventContainer
{
    IObservable<FileRenamedModel> FileRenamedEvent { get; }
    void FileRenamed(string previousName,string newName);
}