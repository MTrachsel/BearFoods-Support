using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BearFoods.BL.Services
{
    public class LogoService : ILogoService
    {
        public void Create(LogoData data)
        {
            PopulateVorlageWithRechnungData(data);
        }

        private static void PopulateVorlageWithRechnungData(LogoData data)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(Bearfoods.LogoPath))
            {
                using (var file = new FileStream("BearFoods.BL.Logo.docx", FileMode.Create, FileAccess.ReadWrite))
                {
                    resource.CopyTo(file);
                    resource.Close();
                    file.Close();
                }
            }

            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open("BearFoods.BL.Logo.docx", true);

            wordprocessingDocument = ReplaceWordArt(data, wordprocessingDocument);

            wordprocessingDocument.Close();
        }

        private static WordprocessingDocument ReplaceWordArt(LogoData data, WordprocessingDocument wpd)
        {
            OpenXmlElement table = wpd.MainDocumentPart.Document.Body.ChildElements.FirstOrDefault();

            foreach (var row in table.ChildElements.Where(x => x.XName.LocalName.Equals("tr")))
            {
                foreach (var cell in row.ChildElements.Where(x => x.XName.LocalName.Equals("tc")))
                {
                    foreach (var paragraph in cell.ChildElements.Where(x => x.XName.LocalName.Equals("p")))
                    {
                        foreach (var run in paragraph.ChildElements.Where(x => x.XName.LocalName.Equals("r")))
                        {
                            foreach (var pic in run.ChildElements.Where(x => x.XName.LocalName.Equals("pict")))
                            {
                                foreach (var shape in pic.ChildElements.Where(x => x.XName.LocalName.Equals("shape")))
                                {
                                    foreach (var textpath in shape.ChildElements.Where(x => x.XName.LocalName.Equals("textpath")))
                                    {
                                        var attr = textpath.GetAttributes().Last();
                                        if (attr.Value.Contains("Batch")) attr.Value = "Batch " + data.BatchNr;
                                        if (attr.Value.Contains("2018")) attr.Value = data.Production.ToShortDateString();
                                        textpath.SetAttribute(attr);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return wpd;
        }        
    }
}
