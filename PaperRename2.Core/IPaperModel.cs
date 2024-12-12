namespace PaperRename2.Core;

public interface IPaperModel
{
    string Title { get; set; }
    string Author { get; set; }
    int Year { get; set; }
    string Name { get; set; }
}