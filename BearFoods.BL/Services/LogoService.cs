using Novacode;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace BearFoods.BL.Services
{
    public class LogoService : ILogoService
    {
        XDocument ILogoService.Create(LogoData data)
        {
            return PopulateVorlageWithRechnungData(data);
        }

        private static XDocument PopulateVorlageWithRechnungData(LogoData data)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Bearfoods.LogoPath);

            DocX document = DocX.Load(stream).Copy();

            XDocument xDoc = XDocument.Parse(document.Xml.ToString());
            xDoc = ReplaceBatch(data, xDoc);
            xDoc = ReplaceDate(data, xDoc);           

            return xDoc;
        }

        private static XDocument ReplaceBatch(LogoData data, XDocument xDoc)
        {
            List<XElement> result = xDoc.Descendants("v")
                            .Where(x => x.Attribute("string")
                                .Value == "Batch 2").ToList();


            foreach (var batch in result)
            {
                batch.Value = "Batch " + data.BatchNr;
            }

            return xDoc;
        }

        private static XDocument ReplaceDate(LogoData data, XDocument xDoc)
        {
            List<XElement> result = xDoc.Descendants("v")
                            .Where(x => x.Attribute("string")
                                .Value == "23.07.2018").ToList();


            foreach (var batch in result)
            {
                batch.Value = data.Production.ToShortDateString();
            }

            return xDoc;
        }
    }
}
