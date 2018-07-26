using BearFoods.BL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacode;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BearFoods.BL.Tests
{
    [TestClass]
    public class LogoServiceTests
    {
        private const string FILE_NAME = "BearFoods.BL.Logo.docx";
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
                BatchNr = "1",
                Production = DateTime.Today
            };

            // Act
            XDocument document = logo.Create(data);
            document.Save(FILE_NAME);

            // Assert
            Assert.IsNotNull(document);
            Assert.IsTrue(File.Exists(FILE_NAME));
        }
    }
}
