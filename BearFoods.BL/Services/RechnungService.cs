using Novacode;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BearFoods.BL.Services
{
    public class RechnungService : IRechnungService
    {
        public DocX Create(RechnungData data)
        {
            DocX document = PopulateVorlageWithRechnungData(data);

            bool hasBBQ = data.MengeBBQ > 0;
            bool hasPizza = data.MengePizza > 0;
            bool hasJus = data.MengeJus > 0;

            if (!hasBBQ) RemoveProductRow(document, Bearfoods.BBQ);

            if (!hasPizza) RemoveProductRow(document, Bearfoods.Pizza);

            if (!hasJus) RemoveProductRow(document, Bearfoods.Jus);

            return document;
        }

        private static void RemoveProductRow(DocX document, string filter)
        {
            Table table = document.Tables.Where(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(filter))))).FirstOrDefault();
            Row row = table.Rows.Where(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(filter)))).FirstOrDefault();
            row.Remove();
        }

        private static DocX PopulateVorlageWithRechnungData(RechnungData data)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Bearfoods.RechnungPath);

            DocX document = DocX.Load(stream).Copy();

            document.ReplaceText(nameof(data.Kunde), data.Kunde ?? string.Empty);
            document.ReplaceText(nameof(data.AdressZeile1), data.AdressZeile1 ?? string.Empty);
            document.ReplaceText(nameof(data.AdressZeile2), data.AdressZeile2 ?? string.Empty);
            document.ReplaceText(nameof(data.Jahr), data.Jahr ?? string.Empty);
            document.ReplaceText(nameof(data.RechnungsNummer), data.RechnungsNummer ?? string.Empty);
            document.ReplaceText(nameof(data.RechnungsDatum), data.RechnungsDatum.ToShortDateString());
            document.ReplaceText(nameof(data.LieferDatum), data.LieferDatum.ToShortDateString());
            document.ReplaceText(nameof(data.MengeBBQ), data.MengeBBQ == 0 ? string.Empty : data.MengeBBQ.ToString());
            document.ReplaceText(nameof(data.MengePizza), data.MengePizza == 0 ? string.Empty : data.MengePizza.ToString());
            document.ReplaceText(nameof(data.MengeJus), data.MengeJus == 0 ? string.Empty : data.MengeJus.ToString());
            document.ReplaceText(nameof(data.EinzelpreisBBQ), data.EinzelpreisBBQ == 0 ? string.Empty : data.EinzelpreisBBQ.ToString());
            document.ReplaceText(nameof(data.EinzelpreisPizza), data.EinzelpreisPizza == 0 ? string.Empty : data.EinzelpreisPizza.ToString());
            document.ReplaceText(nameof(data.EinzelpreisJus), data.EinzelpreisJus == 0 ? string.Empty : data.EinzelpreisJus.ToString());
            document.ReplaceText(nameof(data.TotalBBQ), data.TotalBBQ == 0 ? string.Empty : data.TotalBBQ.ToString());
            document.ReplaceText(nameof(data.TotalPizza), data.TotalPizza == 0 ? string.Empty : data.TotalPizza.ToString());
            document.ReplaceText(nameof(data.TotalJus), data.TotalJus == 0 ? string.Empty : data.TotalJus.ToString());
            document.ReplaceText(nameof(data.SubTotal), data.SubTotal == 0 ? string.Empty : data.SubTotal.ToString());
            document.ReplaceText(nameof(data.Total), data.Total == 0 ? string.Empty : data.Total.ToString());

            return document;
        }
    }
}
