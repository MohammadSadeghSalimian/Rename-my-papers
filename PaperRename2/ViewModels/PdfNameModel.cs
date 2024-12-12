using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PaperRename2.Wpf.ViewModels;

public class PdfNameModel(string name) : ReactiveObject
{
    [Reactive] public string Name { get; set; } = name;
    [Reactive] public bool IsSelected { get; set; }
}