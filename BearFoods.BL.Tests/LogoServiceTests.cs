using BearFoods.BL.Services;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace BearFoods.BL.Tests
{
    [TestClass]
    public class LogoServiceTests
    {
        private const string FILE_NAME = "BearFoods.BL.Logo.docx";
        private const string BATCH_3 = "Batch 3";

        [TestInitialize]
        public void Initialize()
        {
            if (File.Exists(FILE_NAME)) File.Delete(FILE_NAME);
        }

        [TestMethod]
        public void TestCreate()
        {
            // Arrange
            ILogoService logo = new LogoService();
            LogoData data = new LogoData
            {
                BatchNr = "3",
                Production = DateTime.Today
            };

            // Act
            logo.Create(data);

            // Assert
            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open("BearFoods.BL.Logo.docx", false);
            var textPath = wordprocessingDocument.MainDocumentPart.Document.Body.ChildElements[0].ChildElements[2].ChildElements[1].ChildElements[1].ChildElements[3].ChildElements[1].ChildElements[1].ChildElements[3];
            Assert.AreEqual(BATCH_3, textPath.GetAttributes().Last().Value);            
        }       
    }
}
