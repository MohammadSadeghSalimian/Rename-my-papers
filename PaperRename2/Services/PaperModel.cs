using PaperRename2.Core;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PaperRename2.Wpf.Services
{

    public class PaperModel :ReactiveObject, IPaperModel
    {
        [Reactive] public string Title { get; set; }
        [Reactive] public int Year { get; set; }
        [Reactive] public string Author { get; set; }
        [Reactive] public string Name { get; set; }





    }
}