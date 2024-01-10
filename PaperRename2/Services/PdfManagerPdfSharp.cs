using System.Diagnostics;
using System.IO;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace PaperRename2.Services
{
    public class PdfManagerPdfSharp : IPdfManager
    {
        private PdfDocument _document;
        public void LoadPdf(string fileName)
        {

            _document = PdfReader.Open(fileName);
            FileName = new FileInfo(fileName);
        }
        public FileInfo FileName { get; set; }
        public string GetTitle()
        {
            return _document.Info.Title;
        }
        public int GetYear()
        {
            return _document.Info.CreationDate.Year;
        }
        public string GetAuthorName()
        {
            return _document.Info.Author;
        }
       
        public void Close()
        {
            _document?.Dispose();
            _document?.Close();
        }
        public void Rename(string name)
        {
            var old = FileName.FullName;
            var newName = Path.Combine(FileName.Directory.FullName, name);
            File.Move(old, newName);
        }
        public void OpenPdfFile()
        {
            Process.Start(new ProcessStartInfo(FileName.FullName)
            {
                UseShellExecute = true,
            });
        }
    }
}