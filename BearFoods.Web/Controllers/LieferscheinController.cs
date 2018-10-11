using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using BearFoods.BL;
using BearFoods.BL.Services;
using BearFoods.Web.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Novacode;

namespace BearFoods.Web.Controllers
{
    public class LieferscheinController : Controller
    {
        private const string CONTENTTYPEWORD = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        private readonly string FILENAME = $"Lieferschein_{DateTime.Today.ToShortDateString()}.docx";
        private readonly IOptions<PricesConfig> PricesConfig;
        private readonly IOptions<KundenConfig> KundenConfig;
        private readonly IMapper Mapper;

        private ILieferscheinService LieferscheinService => new LieferscheinService();
        private ICalculateService CalculateService => new CalculateService();

        public LieferscheinController(IOptions<PricesConfig> pricesConfiguration, IOptions<KundenConfig> kundenConfiguration, IMapper mapperService)
        {
            PricesConfig = pricesConfiguration;
            KundenConfig = kundenConfiguration;
            Mapper = mapperService;
        } 

        public ActionResult Index()
        {
            LieferscheinViewModel model = SetKundenListe();

            return View(model);
        }        

        public FileStreamResult CreateLieferschein(LieferscheinViewModel model)
        {
            LieferscheinData data = Mapper.Map<LieferscheinData>(model);

            if (model.BestehenderKunde != string.Empty) SetKundenInfo(data, model.BestehenderKunde);
            
            SetPrices(data);
            SetLieferscheinNr(data);
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

        private void SetLieferscheinNr(LieferscheinData data)
        {
            if (System.IO.File.Exists("counters.json"))
            {
                Counters counters;
                using (StreamReader reader = new StreamReader("counters.json"))
                {
                    string jsonCounters = reader.ReadToEnd();
                    counters = JsonConvert.DeserializeObject<Counters>(jsonCounters);
                }

                data.LieferNr = counters.LieferscheinNr.ToString();

                counters.LieferscheinNr += 1; 
                
                string jsonToBeWritten = JsonConvert.SerializeObject(counters);
                System.IO.File.WriteAllText("counters.json", jsonToBeWritten);                              
            }
        }

        private void SetKundenInfo(LieferscheinData data, string bestehenderKunde)
        {
            Kunde kunde = KundenConfig.Value.Kunden.Find(x => x.Name == bestehenderKunde);
            data.KundenName = kunde.Name;
            data.AdressZeile1 = kunde.Adresse1;
            data.AdressZeile2 = kunde.Adresse2;
            data.KundeNr = kunde.KundenNr;
        }

        private void SetPrices(LieferscheinData data)
        {
            
            data.EinzelpreisBBQ = PricesConfig.Value.BBQPrice;
            data.TotalBBQ = data.EinzelpreisBBQ * data.MengeBBQ;

            data.EinzelpreisBBQSmall = PricesConfig.Value.BBQPriceSmall;
            data.TotalBBQSmall = data.EinzelpreisBBQSmall * data.MengeBBQSmall;

            data.EinzelpreisPizza = PricesConfig.Value.PizzaPrice;
            data.TotalPizza = data.EinzelpreisPizza * data.MengePizza;

            data.EinzelpreisJus = PricesConfig.Value.JusPrice;
            data.TotalJus = data.EinzelpreisJus * data.MengeJus;

            data.EinzelpreisJusSmall = PricesConfig.Value.JusPriceSmall;
            data.TotalJusSmall = data.EinzelpreisJusSmall * data.MengeJusSmall;            
        }

        private LieferscheinViewModel SetKundenListe()
        {
            LieferscheinViewModel model = new LieferscheinViewModel
            {
                Kunden = new List<SelectListItem>()
            };

            foreach (Kunde kunde in KundenConfig.Value.Kunden)
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