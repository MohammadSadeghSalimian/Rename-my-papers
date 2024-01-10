using ReactiveUI.Fody.Helpers;
using ReactiveUI;

namespace PaperRename2.Services
{

    public class PaperModel :ReactiveObject, IPaperModel
    {
        [Reactive] public string Title { get; set; }
        [Reactive] public int Year { get; set; }
        [Reactive] public string Author { get; set; }
        [Reactive] public string Name { get; set; }





    }
}