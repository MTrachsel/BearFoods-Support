using BearFoods.BL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BearFoods.BL.Tests
{
    [TestClass]
    public class CalculateServiceTests
    {
        ICalculateService CalculateService => new CalculateService();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCalculatePrices_Rechnung_Fail_With_No_Param()
        {
            // Act
            CalculateService.CalulateRechnungTotals(null);
        }

        [TestMethod]
        public void TestCalculatePrices_Rechnung()
        {
            // Arrange
            RechnungData data = CreateRechnungData();

            // Act
            data = CalculateService.CalulateRechnungTotals(data);

            // Assert
            Assert.AreEqual(76m, data.Total);
            Assert.AreEqual(76m, data.SubTotal);
            Assert.AreEqual(16m, data.TotalBBQ);
            Assert.AreEqual(24m, data.TotalPizza);
            Assert.AreEqual(36m, data.TotalJus);
        }

        [TestMethod]
        public void TestCalculatePrices_Lieferschein()
        {
            // Arrange
            LieferscheinData data = CreateLieferscheinData();

            // Act
            data = CalculateService.CalulateLieferscheinTotals(data);

            // Assert
            Assert.AreEqual(76m, data.Total);
            Assert.AreEqual(16m, data.TotalBBQ);
            Assert.AreEqual(24m, data.TotalPizza);
            Assert.AreEqual(36m, data.TotalJus);
        }

        private static RechnungData CreateRechnungData()
        {
            return new RechnungData
            {
                Kunde = "Metzgerei Siegfried",
                AdressZeile1 = "Bottigenstrasse 22",
                AdressZeile2 = "3018 Bern",
                RechnungsDatum = DateTime.Today,
                LieferDatum = DateTime.Today,
                RechnungsNummer = "0078",
                EinzelpreisBBQ = 8.00m,
                EinzelpreisPizza = 6.00m,
                EinzelpreisJus = 12.00m,
                MengeBBQ = 2,
                MengeJus = 3,
                MengePizza = 4
            };
        }

        private static LieferscheinData CreateLieferscheinData()
        {
            return new LieferscheinData
            {
                KundenName = "Metzgerei Siegfried",
                KundeNr = "25",
                AdressZeile1 = "Bottigenstrasse 22",
                AdressZeile2 = "3018 Bern",
                LieferNr = "20",
                LieferDatum = DateTime.Today,
                EinzelpreisBBQ = 8.00m,
                EinzelpreisPizza = 6.00m,
                EinzelpreisJus = 12.00m,
                MengeBBQ = 2,
                MengeJus = 3,
                MengePizza = 4
            };
        }

    }
}
