
using PaperRename2.Pdf;
using Xunit;

namespace PaperRename2.Tests
{
    
    public class RemoveProtectionTests
    {
        [Fact]
        public void TestRemovingFileProtection()
        {
            var address =
                @"C:\Users\msal550\Downloads\440.7-22.pdf";
            var rp = new ProtectionRemover();
            rp.Remove(new FileInfo(address));
        }
        
    }

   
}
