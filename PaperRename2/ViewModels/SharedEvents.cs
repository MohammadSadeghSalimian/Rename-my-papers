using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using PaperRename2.Wpf.Models;

namespace PaperRename2.Wpf.ViewModels;

public class SharedEvents:ISharedEvents
{
    public IObservable<FileRenamedModel> FileRenamedEvent => _renameEvent.AsObservable();
    private readonly Subject<FileRenamedModel> _renameEvent = new();

    public void FileRenamed(string previousName,string newName)
    {
        try
        {
            _renameEvent.OnNext(new FileRenamedModel(previousName, newName));
        }
        catch (Exception e)
        {
           _renameEvent.OnError(e);
        }
       
    }
}