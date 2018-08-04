using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BearFoods.Web.Models;
using Novacode;
using System.IO;
using BearFoods.BL.Services;
using BearFoods.BL;

namespace BearFoods.Web.Controllers
{
    public class LogoController : Controller
    {
        private const string CONTENTTYPEWORD = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        private ILogoService LogoService => new LogoService();
        
        public IActionResult Index()
        {
            return View();
        }

        public FileStreamResult CreateLogo(LogoModel model)
        {
            string FILENAME = $"Logo_{model.Production.ToShortDateString()}-BatchNr_{model.BatchNr}.docx";

            LogoData data = MapModelToData(model);

            DocX doc = LogoService.Create(data);

            MemoryStream ms = new MemoryStream();
            doc.SaveAs(ms);
            ms.Position = 0;

            var file = new FileStreamResult(ms, CONTENTTYPEWORD)
            {
                FileDownloadName = string.Format(FILENAME)
            };

            return file;
        }

        private static LogoData MapModelToData(LogoModel model)
        {
            return new LogoData
            {
                BatchNr = model.BatchNr.ToString(),
                Production = model.Production
            };
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
