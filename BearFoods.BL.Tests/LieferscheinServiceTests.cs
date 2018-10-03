using BearFoods.BL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacode;
using System;
using System.IO;
using System.Linq;

namespace BearFoods.BL.Tests
{
    [TestClass]
    public class LieferscheinServiceTests
    {
        private const string FILE_NAME = "BearFoods.BL.Lieferschein.docx";
        [TestInitialize]
        public void Initialize()
        {
            if (File.Exists(FILE_NAME)) File.Delete(FILE_NAME);
        }

        ILieferscheinService LieferscheinService => new LieferscheinService();

        [TestMethod]
        public void TestCreate_Full()
        {
            // Arrange
            LieferscheinData data = CreateLieferscheinData();

            // Act
            DocX document = LieferscheinService.Create(data);
            document.SaveAs(FILE_NAME);

            // Assert
            Assert.IsNotNull(document);
            Assert.IsTrue(File.Exists(FILE_NAME));
        }

        [TestMethod]
        public void TestCreate_One_Product_BBQ()
        {
            // Arrange
            LieferscheinData data = CreateLieferscheinData();
            data.LieferDatum = DateTime.Today;
            data.LieferNr = "0079";
            data.EinzelpreisBBQ = 10.00m;
            data.TotalBBQ = 20.00m;
            data.MengeBBQ = 2;
            data.Total = 20.00m;
            data.MengeJus = 0;
            data.MengePizza = 0;
            
            // Act
            DocX document = LieferscheinService.Create(data);
            document.SaveAs(FILE_NAME);

            // Assert
            Assert.IsNotNull(document);
            Assert.IsTrue(File.Exists(FILE_NAME));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Jus))))));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Pizza)))))); 
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.JusSmall))))));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.BBQSmall))))));
            Assert.IsTrue(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.BBQ))))));
        }

        [TestMethod]
        public void TestCreate_One_Product_Pizza()
        {
            // Arrange
            LieferscheinData data = CreateLieferscheinData();
            data.LieferDatum = DateTime.Today;
            data.LieferNr = "0079";
            data.EinzelpreisPizza = 6.00m;
            data.TotalPizza = 12.00m;
            data.MengePizza = 2;
            data.Total = 12.00m;
            data.MengeBBQ = 0;
            data.MengeJus = 0;

            // Act
            DocX document = LieferscheinService.Create(data);
            document.SaveAs(FILE_NAME);

            // Assert
            Assert.IsNotNull(document);
            Assert.IsTrue(File.Exists(FILE_NAME));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Jus))))));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.BBQ))))));
            Assert.IsTrue(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Pizza))))));
        }

        [TestMethod]
        public void TestCreate_One_Product_Jus()
        {
            // Arrange
            LieferscheinData data = CreateLieferscheinData();
            data.LieferDatum = DateTime.Today;
            data.LieferNr = "0079";
            data.EinzelpreisJus = 12.00m;
            data.TotalJus = 24.00m;
            data.MengeJus = 2;
            data.Total = 24.00m;
            data.MengeBBQ = 0;
            data.MengePizza = 0;

            // Act
            DocX document = LieferscheinService.Create(data);
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
            LieferscheinData data = CreateLieferscheinData();
            data.LieferDatum = DateTime.Today;
            data.LieferNr = "0079";
            data.EinzelpreisJus = 12.00m;
            data.EinzelpreisBBQ = 10.00m;
            data.TotalJus = 24.00m;
            data.TotalBBQ = 30.00m;
            data.MengeJus = 2;
            data.MengeBBQ = 3;
            data.Total = 54.00m;
            data.MengePizza = 0;

            // Act
            DocX document = LieferscheinService.Create(data);
            document.SaveAs(FILE_NAME);

            // Assert
            Assert.IsNotNull(document);
            Assert.IsTrue(File.Exists(FILE_NAME));
            Assert.IsFalse(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Pizza))))));
            Assert.IsTrue(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.BBQ))))));
            Assert.IsTrue(document.Tables.Any(x => x.Rows.Any(y => y.Cells.Any(p => p.Paragraphs.Any(t => t.Text.Contains(Bearfoods.Jus))))));
        }

        private static LieferscheinData CreateLieferscheinData()
        {
            return new LieferscheinData
            {
                KundenName = "Metzgerei Siegfried",
                AdressZeile1 = "Bottigenstrasse 22",
                AdressZeile2 = "3018 Bern",
                LieferDatum = DateTime.Today,
                KundeNr = "25",
                LieferNr = "77",
                EinzelpreisBBQ = 8.00m,
                EinzelpreisPizza = 6.00m,
                EinzelpreisJus = 12.00m,
                TotalBBQ = 20.00m,
                TotalPizza = 18.00m,
                TotalJus = 48.00m,
                MengeBBQ = 2,
                MengeJus = 4,
                MengePizza = 3,
                Total = 90.00m
            };
        }
    }
}
