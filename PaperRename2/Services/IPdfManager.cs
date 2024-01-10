using System.IO;

namespace PaperRename2.Services
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
}