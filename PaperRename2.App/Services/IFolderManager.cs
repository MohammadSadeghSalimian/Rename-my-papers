namespace PaperRename2.App.Services;

public interface IFolderManager
{
    DirectoryInfo RootFolder { get; set; }
    IEnumerable<FileInfo> GetPdfFiles(DirectoryInfo root);
    FileInfo GetFileByName(string name);
}