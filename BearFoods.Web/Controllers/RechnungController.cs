using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BearFoods.Web.Models;
using Novacode;
using System.IO;
using System;
using Microsoft.Extensions.Options;
using BearFoods.Web.Config;
using BearFoods.BL.Services;
using BearFoods.BL;

namespace BearFoods.Web.Controllers
{
    public class RechnungController : Controller
    {
        private const string CONTENTTYPEWORD = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        private readonly IOptions<PricesConfig> config;
        private IRechnungService RechnungService => new RechnungService();
        private ICalculateService CalculateService => new CalculateService();

        public RechnungController(IOptions<PricesConfig> configuration) => config = configuration;

        public IActionResult Index()
        {
            return View();
        }

        public FileStreamResult CreateRechnung(RechnungData data)
        {
            string FILENAME = $"Rechnung_{data.Jahr}-{data.RechnungsNummer}.docx";

            SetPrices(data);
            data = CalculateService.CalulateRechnungTotals(data);

            DocX doc = RechnungService.Create(data);
            
            MemoryStream ms = new MemoryStream();
            doc.SaveAs(ms);
            ms.Position = 0;

            var file = new FileStreamResult(ms, CONTENTTYPEWORD)
            {
                FileDownloadName = string.Format(FILENAME)
            };

            return file;
        }

        private void SetPrices(RechnungData data)
        {

            data.EinzelpreisBBQ = config.Value.BBQPrice;
            data.EinzelpreisPizza = config.Value.PizzaPrice;
            data.EinzelpreisJus = config.Value.JusPrice;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
