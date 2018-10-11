using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using BearFoods.BL;
using BearFoods.BL.Services;
using BearFoods.Web.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Novacode;

namespace BearFoods.Web.Controllers
{
    public class LieferscheinController : Controller
    {
        private const string CONTENTTYPEWORD = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        private string FILENAME = $"Lieferschein_{DateTime.Today.ToShortDateString()}.docx";
        private readonly IOptions<PricesConfig> pricesConfig;
        private readonly IOptions<KundenConfig> kundenConfig;

        private ILieferscheinService LieferscheinService => new LieferscheinService();
        private ICalculateService CalculateService => new CalculateService();

        public LieferscheinController(IOptions<PricesConfig> pricesConfiguration, IOptions<KundenConfig> kundenConfiguration)
        {
            pricesConfig = pricesConfiguration;
            kundenConfig = kundenConfiguration;
        } 

        public ActionResult Index()
        {
            LieferscheinViewModel model = SetKundenListe();

            return View(model);
        }        

        public FileStreamResult CreateLieferschein(LieferscheinViewModel model)
        {
            SetPrices(model);

            Mapper.Initialize(cfg => cfg.CreateMap<LieferscheinViewModel, LieferscheinData>());
            LieferscheinData data = Mapper.Map<LieferscheinData>(model);

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

        private void SetPrices(LieferscheinViewModel model)
        {
            
            model.EinzelpreisBBQ = pricesConfig.Value.BBQPrice;
            model.TotalBBQ = model.EinzelpreisBBQ * model.MengeBBQ;

            model.EinzelpreisBBQSmall = pricesConfig.Value.BBQPriceSmall;
            model.TotalBBQSmall = model.EinzelpreisBBQSmall * model.MengeBBQSmall;

            model.EinzelpreisPizza = pricesConfig.Value.PizzaPrice;
            model.TotalPizza = model.EinzelpreisPizza * model.MengePizza;

            model.EinzelpreisJus = pricesConfig.Value.JusPrice;
            model.TotalJus = model.EinzelpreisJus * model.MengeJus;

            model.EinzelpreisJusSmall = pricesConfig.Value.JusPriceSmall;
            model.TotalJusSmall = model.EinzelpreisJusSmall * model.MengeJusSmall;            
        }

        private LieferscheinViewModel SetKundenListe()
        {
            LieferscheinViewModel model = new LieferscheinViewModel
            {
                Kunden = new List<SelectListItem>()
            };

            foreach (Kunde kunde in kundenConfig.Value.Kunden)
            {
                model.Kunden.Add(new SelectListItem
                {
                    Text = kunde.Name,
                    Value = kunde.Name
                });
            }

            return model;
        }
    }
}