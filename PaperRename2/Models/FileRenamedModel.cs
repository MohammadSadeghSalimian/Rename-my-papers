using System.IO;

namespace PaperRename2.Models;

public class FileRenamedModel
{
    public FileRenamedModel(string previousName, string newName)
    {
        PreviousName = previousName;
        NewName = newName;
    }
    public string PreviousName { get; }
    public string NewName { get; }

}