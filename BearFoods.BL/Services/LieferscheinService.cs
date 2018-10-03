using Novacode;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BearFoods.BL.Services
{
    public class LieferscheinService : ILieferscheinService
    {
        public DocX Create(LieferscheinData data)
        {
            DocX document = PopulateVorlageWithRechnungData(data);

            bool hasBBQ = data.MengeBBQ > 0; 
            bool hasPizza = data.MengePizza > 0;
            bool hasJus = data.MengeJus > 0;
            bool hasJusSmall = data.MengeJusSmall > 0;
            bool hasBBQSmall = data.MengeBBQSmall > 0;

            if (!hasBBQ) RemoveProductRow(document, Bearfoods.BBQ);

            if (!hasPizza) RemoveProductRow(document, Bearfoods.Pizza);

            if (!hasJus) RemoveProductRow(document, Bearfoods.Jus);

            if (!hasJusSmall) RemoveProductRow(document, Bearfoods.JusSmall);

            if (!hasBBQSmall) RemoveProductRow(document, Bearfoods.BBQSmall);

            return document;
        }

        private static void RemoveProductRow(DocX document, string filter)
        {
            Table table = document.Tables.Where(x => x.Rows.Any(y => y.Cells.Any(p=>p.Paragraphs.Any(t => t.Text.Contains(filter))))).FirstOrDefault();
            if (table == null) return;

            Row row = table.Rows.Where(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(filter)))).FirstOrDefault();
            row.Remove();
        }

        private static DocX PopulateVorlageWithRechnungData(LieferscheinData data)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Bearfoods.LieferscheinPath);

            DocX document = DocX.Load(stream).Copy();

            document.ReplaceText(nameof(data.KundenName), data.KundenName ?? string.Empty);
            document.ReplaceText(nameof(data.AdressZeile1), data.AdressZeile1 ?? string.Empty);
            document.ReplaceText(nameof(data.AdressZeile2), data.AdressZeile2 ?? string.Empty);
            document.ReplaceText(nameof(data.LieferNr), data.LieferNr ?? string.Empty);
            document.ReplaceText(nameof(data.KundeNr), data.KundeNr ?? string.Empty);
            document.ReplaceText(nameof(data.LieferDatum), data.LieferDatum.ToShortDateString());
            document.ReplaceText(nameof(data.MengeBBQ), data.MengeBBQ == 0 ? string.Empty : data.MengeBBQ.ToString());
            document.ReplaceText(nameof(data.MengeBBQSmall), data.MengeBBQSmall == 0 ? string.Empty : data.MengeBBQSmall.ToString());
            document.ReplaceText(nameof(data.MengePizza), data.MengePizza == 0 ? string.Empty : data.MengePizza.ToString());
            document.ReplaceText(nameof(data.MengeJus), data.MengeJus == 0 ? string.Empty : data.MengeJus.ToString());
            document.ReplaceText(nameof(data.MengeJusSmall), data.MengeJusSmall == 0 ? string.Empty : data.MengeJusSmall.ToString());
            document.ReplaceText(nameof(data.EinzelpreisBBQ), data.EinzelpreisBBQ == 0 ? string.Empty : data.EinzelpreisBBQ.ToString());
            document.ReplaceText(nameof(data.EinzelpreisBBQSmall), data.EinzelpreisBBQSmall == 0 ? string.Empty : data.EinzelpreisBBQSmall.ToString());
            document.ReplaceText(nameof(data.EinzelpreisPizza), data.EinzelpreisPizza == 0 ? string.Empty : data.EinzelpreisPizza.ToString());
            document.ReplaceText(nameof(data.EinzelpreisJus), data.EinzelpreisJus == 0 ? string.Empty : data.EinzelpreisJus.ToString());
            document.ReplaceText(nameof(data.EinzelpreisJusSmall), data.EinzelpreisJusSmall == 0 ? string.Empty : data.EinzelpreisJusSmall.ToString());
            document.ReplaceText(nameof(data.TotalBBQ), data.TotalBBQ == 0 ? string.Empty : data.TotalBBQ.ToString());
            document.ReplaceText(nameof(data.TotalBBQSmall), data.TotalBBQSmall == 0 ? string.Empty : data.TotalBBQSmall.ToString());
            document.ReplaceText(nameof(data.TotalPizza), data.TotalPizza == 0 ? string.Empty : data.TotalPizza.ToString());
            document.ReplaceText(nameof(data.TotalJus), data.TotalJus == 0 ? string.Empty : data.TotalJus.ToString());
            document.ReplaceText(nameof(data.TotalJusSmall), data.TotalJusSmall == 0 ? string.Empty : data.TotalJusSmall.ToString());
            document.ReplaceText(nameof(data.Total), data.Total == 0 ? string.Empty : data.Total.ToString());

            return document;
        }        
    }
}
