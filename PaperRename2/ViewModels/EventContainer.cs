using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using PaperRename2.Models;

namespace PaperRename2.ViewModels;

public class EventContainer:IEventContainer
{
    public IObservable<FileRenamedModel> FileRenamedEvent => _renameEvent.AsObservable();
    private readonly Subject<FileRenamedModel> _renameEvent;
    public EventContainer()
    {
        _renameEvent=new Subject<FileRenamedModel>();
    }
    public void FileRenamed(string previousName,string newName)
    {
        _renameEvent.OnNext(new FileRenamedModel(previousName,newName));
    }
}