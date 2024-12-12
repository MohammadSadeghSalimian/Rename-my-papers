namespace PaperRename2.App.Services
{
    public interface IPdfManager
    {
        void LoadPdf(string fileName);
        string GetTitle();
        int GetYear();
        string GetAuthorName();
        FileInfo FileName { get; set; }
        void Close();
        void Rename(string name);
        void OpenPdfFile();
    }


    public interface IProtectionRemover
    {
        void Remove(FileInfo file);
    }
}