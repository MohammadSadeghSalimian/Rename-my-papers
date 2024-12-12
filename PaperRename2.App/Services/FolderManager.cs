using PaperRename2.App.Services;

public class FolderManager : IFolderManager
{
    public DirectoryInfo RootFolder { get; set; }

    public IEnumerable<FileInfo> GetPdfFiles(DirectoryInfo root)
    {
        var names = root.GetFiles().Where(x => x.Extension.ToLower() == ".pdf").OrderBy(x => x.Name);
        return names;
    }

    public FileInfo GetFileByName(string name)
    {
        var f = new FileInfo(Path.Combine(RootFolder.FullName, name));
        return f.Exists ? f : null;
    }

    public async Task SendRenamedFolder()
    {

    }
}