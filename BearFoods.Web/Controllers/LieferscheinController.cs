using System;
using System.IO;
using BearFoods.BL;
using BearFoods.BL.Services;
using BearFoods.Web.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Novacode;

namespace BearFoods.Web.Controllers
{
    public class LieferscheinController : Controller
    {
        private const string CONTENTTYPEWORD = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        private string FILENAME = $"Lieferschein_{DateTime.Today.ToShortDateString()}.docx";
        private readonly IOptions<PricesConfig> config;

        private ILieferscheinService LieferscheinService => new LieferscheinService();
        private ICalculateService CalculateService => new CalculateService();

        public LieferscheinController(IOptions<PricesConfig> configuration) => config = configuration;
        public ActionResult Index()
        {
            return View();
        }

        public FileStreamResult CreateLieferschein(LieferscheinData data)
        {
            SetPrices(data);
            data = CalculateService.CalulateLieferscheinTotals(data);

            DocX doc = LieferscheinService.Create(data);

            MemoryStream ms = new MemoryStream();
            doc.SaveAs(ms);
            ms.Position = 0;

            var file = new FileStreamResult(ms, CONTENTTYPEWORD)
            {
                FileDownloadName = string.Format(FILENAME)
            };

            return file;
        }

        private void SetPrices(LieferscheinData data)
        {
            
            data.EinzelpreisBBQ = config.Value.BBQPrice;
            data.TotalBBQ = data.EinzelpreisBBQ * data.MengeBBQ;

            data.EinzelpreisPizza = config.Value.PizzaPrice;
            data.TotalPizza = data.EinzelpreisPizza * data.MengePizza;

            data.EinzelpreisJus = config.Value.JusPrice;
            data.TotalJus = data.EinzelpreisJus * data.MengeJus;

            data.Total = data.TotalPizza + data.TotalPizza + data.TotalJus;
        }
    }
}