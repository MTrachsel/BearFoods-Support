using System.IO;
using System.Linq;
using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Novacode;

namespace BearFoods.BL.Services
{
    public class LogoService : ILogoService
    {
        public DocX Create(LogoData data)
        {
            PopulateVorlageWithRechnungData(data);

            return DocX.Load(Bearfoods.LogoInstancePath).Copy();
        }

        private static void PopulateVorlageWithRechnungData(LogoData data)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(Bearfoods.LogoPath))
            {
                using (var file = new FileStream(Bearfoods.LogoInstancePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    resource.CopyTo(file);
                    resource.Close();
                    file.Close();
                }
            }

            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(Bearfoods.LogoInstancePath, true);

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
