using Novacode;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace BearFoods.BL.Services
{
    public class LogoService : ILogoService
    {
        public DocX Create(LogoData data)
        {
            return PopulateVorlageWithRechnungData(data);
        }

        private static DocX PopulateVorlageWithRechnungData(LogoData data)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Bearfoods.LogoPath);

            DocX document = DocX.Load(stream).Copy();

            XDocument xDoc = XDocument.Parse(document.Xml.ToString());
            xDoc = ReplaceBatch(data, xDoc);
            xDoc = ReplaceDate(data, xDoc);           

            return document;
        }

        private static XDocument ReplaceBatch(LogoData data, XDocument xDoc)
        {
            List<XElement> result = xDoc.Descendants("{urn:schemas-microsoft-com:vml}textpath")
                            .Where(x => x.Attribute("string") != null 
                                   && x.Attribute("string").Value == Bearfoods.Batch).ToList();

            foreach (var batch in result)
            {
                batch.LastAttribute.Value = "Batch " + data.BatchNr;
            }

            return xDoc;
        }

        private static XDocument ReplaceDate(LogoData data, XDocument xDoc)
        {
            List<XElement> result = xDoc.Descendants("{urn:schemas-microsoft-com:vml}textpath")
                            .Where(x => x.Attribute("string") != null 
                                   && x.Attribute("string").Value == Bearfoods.Date).ToList();
            
            foreach (var batch in result)
            {
                batch.LastAttribute.Value = data.Production.ToShortDateString();
            }

            return xDoc;
        }
    }
}
