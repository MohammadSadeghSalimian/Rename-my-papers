using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using PaperRename2.App.Services;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Writer;

namespace PaperRename2.Pdf
{

    public class ProtectionRemover:IProtectionRemover
    {
        public void Remove(FileInfo file)
        {
            if (file.Directory == null)
            {
                return;
            }
            using var pp = PdfDocument.Open(file.FullName);
            using var bb = new PdfDocumentBuilder();

            for (int i = 0; i < pp.NumberOfPages; i++)
            {
                bb.AddPage(pp, i + 1);
            }

            var newName = file.Name.ToLower().Replace(".pdf", "-NO RESTRICTION.pdf", true, CultureInfo.InvariantCulture);
            var newFile = new FileInfo(Path.Combine(file.Directory.FullName, newName));
            File.WriteAllBytes(newFile.FullName, bb.Build());
        }

        public void MergePdf(List<FileInfo> sources, FileInfo file)
        {
           
            using var bb = new PdfDocumentBuilder();
            foreach (FileInfo source in sources)
            {
                using var pp = PdfDocument.Open(source.FullName);
                for (int i = 0; i < pp.NumberOfPages; i++)
                {
                    
                    bb.AddPage(pp, i + 1);
                }
            }
            File.WriteAllBytes(file.FullName, bb.Build());
        }

        public void SeparatePdf(FileInfo file)
        {
            if (file.Directory == null)
            {
                return;
            }

            var folderName = Path.GetFileNameWithoutExtension(file.FullName);
            var root = Path.Combine(file.Directory.FullName, folderName);
            Directory.CreateDirectory(root);
            using var pp = PdfDocument.Open(file.FullName);
   
            for (int i = 0; i < pp.NumberOfPages; i++)
            {
                using var bb = new PdfDocumentBuilder();
                bb.AddPage(612, 792);
                bb.AddPage(pp, i + 1);
                var fileName = $"Page {i + 1} {file.Name}";
                var newFile = new FileInfo(Path.Combine(root, fileName));
                File.WriteAllBytes(newFile.FullName, bb.Build());

            }
        }
    }
}
