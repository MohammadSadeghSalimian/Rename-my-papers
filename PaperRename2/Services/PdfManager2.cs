//using System.Diagnostics;
//using System.IO;
//using ABI.System;
//using PdfiumViewer;

//namespace PaperRename2.Services;

//public class PdfManager2 : IPdfManager,IDisposable
//{
//    private PdfDocument _document;

//    public void LoadPdf(string fileName)
//    {
//        _document = PdfDocument.Load(fileName);
//        FileName = new FileInfo(fileName);
//    }

//    public string GetTitle()
//    {
//         return _document.GetInformation().Title;
//    }

//    public int GetYear()
//    {
//        var time= _document.GetInformation().CreationDate;
//        return time?.Year ?? 0;
//    }

//    public string GetAuthorName()
//    {
//       return _document.GetInformation().Author;
//    }

//    public FileInfo FileName { get; set; }
//    public void Close()
//    {
//        _document?.Dispose();
        
//    }

//    public void Rename(string name)
//    {
//        var old = FileName.FullName;
//        var newName = Path.Combine(FileName.Directory.FullName, name);
//        File.Move(old, newName);
//    }

//    public void OpenPdfFile()
//    {
//        Process.Start(new ProcessStartInfo(FileName.FullName)
//        {
//            UseShellExecute = true,
//        });
//    }
//}