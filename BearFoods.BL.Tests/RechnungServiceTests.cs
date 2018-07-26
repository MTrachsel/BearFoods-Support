using BearFoods.BL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacode;
using System;
using System.IO;
using System.Linq;

namespace BearFoods.BL.Tests
{
    [TestClass]
    public class RechnungServiceTests
    {
        private const string FILE_NAME = "BearFoods.BL.Rechnung.docx";
        [TestInitialize]
        public void Initialize()
        {
            if (File.Exists(FILE_NAME)) File.Delete(FILE_NAME);
        }

        [TestMethod]
        public void TestCreate_Full()
        {
            // Arrange
            IRechnungService rechnung = new RechnungService();
            RechnungData data = CreateRechnungData();

            // Act
            DocX document = rechnung.Create(data);
            document.SaveAs(FILE_NAME);

            // Assert
            Assert.IsNotNull(document);
            Assert.IsTrue(File.Exists(FILE_NAME));
        }

        [TestMethod]
        public void TestCreate_One_Product_BBQ()
        {
            // Arrange
            IRechnungService rechnung = new RechnungService();
            RechnungData data = new RechnungData
            {
                Kunde = "Metzgerei Siegfried",
                AdressZeile1 = "Bottigenstrasse 22",
                AdressZeile2 = "3018 Bern",
                RechnungsDatum = DateTime.Today,
                LieferDatum = DateTime.Today,
                RechnungsNummer = "0079",
                EinzelpreisBBQ = 10.00m,
                TotalBBQ = 20.00m,
                MengeBBQ = 2,
                SubTotal = 20.00m,
                Total = 20.00m
            };
            
            // Act
            DocX document = rechnung.Create(data);
            document.SaveAs(FILE_NAME);

            // Assert
            Assert.IsNotNull(document);
            Assert.IsTrue(File.Exists(FILE_NAME));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Pizza))))));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Jus))))));
            Assert.IsTrue(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.BBQ))))));


        }

        [TestMethod]
        public void TestCreate_One_Product_Pizza()
        {
            // Arrange
            IRechnungService rechnung = new RechnungService();
            RechnungData data = new RechnungData
            {
                Kunde = "Metzgerei Siegfried",
                AdressZeile1 = "Bottigenstrasse 22",
                AdressZeile2 = "3018 Bern",
                RechnungsDatum = DateTime.Today,
                LieferDatum = DateTime.Today,
                RechnungsNummer = "0079",
                EinzelpreisPizza = 10.00m,
                TotalPizza = 20.00m,
                MengePizza = 2,
                SubTotal = 20.00m,
                Total = 20.00m
            };

            // Act
            DocX document = rechnung.Create(data);
            document.SaveAs(FILE_NAME);

            // Assert
            Assert.IsNotNull(document);
            Assert.IsTrue(File.Exists(FILE_NAME));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.BBQ))))));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Jus))))));
            Assert.IsTrue(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Pizza))))));

        }

        [TestMethod]
        public void TestCreate_One_Product_Jus()
        {
            // Arrange
            IRechnungService rechnung = new RechnungService();
            RechnungData data = new RechnungData
            {
                Kunde = "Metzgerei Siegfried",
                AdressZeile1 = "Bottigenstrasse 22",
                AdressZeile2 = "3018 Bern",
                RechnungsDatum = DateTime.Today,
                LieferDatum = DateTime.Today,
                RechnungsNummer = "0079",
                EinzelpreisJus = 10.00m,
                TotalJus = 20.00m,
                MengeJus = 2,
                SubTotal = 20.00m,
                Total = 20.00m
            };

            // Act
            DocX document = rechnung.Create(data);
            document.SaveAs(FILE_NAME);

            // Assert
            Assert.IsNotNull(document);
            Assert.IsTrue(File.Exists(FILE_NAME));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Pizza))))));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.BBQ))))));
            Assert.IsTrue(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Jus))))));

        }

        [TestMethod]
        public void TestCreate_Two_Products()
        {
            // Arrange
            IRechnungService rechnung = new RechnungService();
            RechnungData data = new RechnungData
            {
                Kunde = "Metzgerei Siegfried",
                AdressZeile1 = "Bottigenstrasse 22",
                AdressZeile2 = "3018 Bern",
                RechnungsDatum = DateTime.Today,
                LieferDatum = DateTime.Today,
                RechnungsNummer = "0079",
                EinzelpreisJus = 12.00m,
                EinzelpreisBBQ = 10.00m,
                TotalJus = 24.00m,
                TotalBBQ = 30.00m,
                MengeJus = 2,
                MengeBBQ = 3,
                SubTotal = 50.00m,
                Total = 50.00m
            };

            // Act
            DocX document = rechnung.Create(data);
            document.SaveAs(FILE_NAME);

            // Assert
            Assert.IsNotNull(document);
            Assert.IsTrue(File.Exists(FILE_NAME));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Pizza))))));
            Assert.IsTrue(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Jus))))));
            Assert.IsTrue(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.BBQ))))));

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
                TotalBBQ = 20.00m,
                TotalPizza = 18.00m,
                TotalJus = 48.00m,
                MengeBBQ = 2,
                MengeJus = 4,
                MengePizza = 3,
                SubTotal = 86.00m,
                Total = 90.00m

            };
        }
    }
}
