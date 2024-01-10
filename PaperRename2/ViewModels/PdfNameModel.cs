using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PaperRename2.ViewModels;

public class PdfNameModel : ReactiveObject
{
    [Reactive] public string Name { get; set; }
    [Reactive] public bool IsSelected { get; set; }

    public PdfNameModel(string name)
    {
        Name = name;

    }
}