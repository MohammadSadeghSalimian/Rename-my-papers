namespace PaperRename2.Wpf.Models;

public class FileRenamedModel(string previousName, string newName)
{
    public string PreviousName { get; } = previousName;
    public string NewName { get; } = newName;
}