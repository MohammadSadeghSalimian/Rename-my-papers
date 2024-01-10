using System.Windows;
using PaperRename2.Services;

namespace PaperRename2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var a = new AppBootStrapWpf();
            a.Setup();
           
        }
    }
}
