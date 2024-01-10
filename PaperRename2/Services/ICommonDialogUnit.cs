namespace PaperRename2.Services
{
    public interface ICommonDialogUnit
    {
        string Title { get; set; }
        string DefaultExtension { get; set; }
        void SetFilters(params string[] filters);
        bool OpenFileDialog(out string fileName);
        bool SaveFileDialog(out string fileName);
        bool OpenFolderDialog(out string fileName);
    }
}