namespace PaperRename2.Wpf.Services
{
    public class DialogBuilder : ICommonDialogBuilder
    {
        public ICommonDialogUnit GetDialog()
        {
            return new CommonDialogUnit();
        }
    }
}