using System;
using System.Diagnostics;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;

namespace PDFProtect
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Get a fresh copy of the sample PDF file.
                const string filenameSource = "BLKey.pdf";
                const string filenameDest = "BLKey.pdf";
                File.Copy(Path.Combine("C:\\Users\\knasdmd\\Downloads", filenameSource),
                  Path.Combine(Directory.GetCurrentDirectory(), filenameDest), true);

                // Open an existing document. Providing an unrequired password is ignored.
                PdfDocument document = PdfReader.Open(filenameDest, "no_password_required");

                PdfSecuritySettings securitySettings = document.SecuritySettings;

                // Setting one of the passwords automatically sets the security level to 
                // PdfDocumentSecurityLevel.Encrypted128Bit.
                securitySettings.UserPassword = "";
                securitySettings.OwnerPassword = "";

                // Don't use 40 bit encryption unless needed for compatibility
                //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

                // Restrict some rights.
                securitySettings.PermitAccessibilityExtractContent = false;
                securitySettings.PermitAnnotations = false;
                securitySettings.PermitAssembleDocument = false;
                securitySettings.PermitExtractContent = false;
                securitySettings.PermitFormsFill = true;
                securitySettings.PermitFullQualityPrint = false;
                securitySettings.PermitModifyDocument = true;
                securitySettings.PermitPrint = false;

                // Save the document...
                document.Save(filenameDest);
                // ...and start a viewer.
                Process.Start(filenameDest);

                Console.WriteLine("Password assigned");
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
     
    }
}
