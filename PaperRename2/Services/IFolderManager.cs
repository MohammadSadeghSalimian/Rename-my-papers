using System.Collections.Generic;
using System.IO;

namespace PaperRename2.Services;

public interface IFolderManager
{
    DirectoryInfo RootFolder { get; set; }
    IEnumerable<FileInfo> GetPdfFiles(DirectoryInfo root);
    FileInfo GetFileByName(string name);
}