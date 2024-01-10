namespace PaperRename2.Services
{
    public class DialogBuilder : ICommonDialogBuilder
    {
        public ICommonDialogUnit GetDialog()
        {
            return new CommonDialogUnit();
        }
    }
}