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
        private const string FILE_NAME2 = "BearFoods.BL.Logo2.docx";
        [TestInitialize]
        public void Initialize()
        {
            if (File.Exists(FILE_NAME)) File.Delete(FILE_NAME);
            if (File.Exists(FILE_NAME2)) File.Delete(FILE_NAME2);
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

        }       
    }
}
