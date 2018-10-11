﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BearFoods.Web.Models;
using Novacode;
using System.IO;
using System;
using Microsoft.Extensions.Options;
using BearFoods.Web.Config;
using BearFoods.BL.Services;
using BearFoods.BL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BearFoods.Web.Controllers
{
    public class RechnungController : Controller
    {
        private const string CONTENTTYPEWORD = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        private readonly IOptions<PricesConfig> pricesConfig;
        private readonly IOptions<KundenConfig> kundenConfig;
        private IRechnungService RechnungService => new RechnungService();
        private ICalculateService CalculateService => new CalculateService();
        
        public RechnungController(IOptions<PricesConfig> pricesConfiguration, IOptions<KundenConfig> kundenConfiguration)
        {
            pricesConfig = pricesConfiguration;
            kundenConfig = kundenConfiguration;
        }

        public IActionResult Index()
        {
            RechnungViewModel model = SetKundenListe();
            return View(model);
        }

        public FileStreamResult CreateRechnung(RechnungViewModel model)
        {
            string FILENAME = $"Rechnung_{model.RechnungsNummer}.docx";

            Mapper.Initialize(cfg => cfg.CreateMap<RechnungViewModel, RechnungData>());
            RechnungData data = Mapper.Map<RechnungData>(model);

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

            data.EinzelpreisBBQ = pricesConfig.Value.BBQPrice;
            data.EinzelpreisBBQSmall = pricesConfig.Value.BBQPriceSmall;
            data.EinzelpreisPizza = pricesConfig.Value.PizzaPrice;
            data.EinzelpreisJus = pricesConfig.Value.JusPrice;
            data.EinzelpreisJusSmall = pricesConfig.Value.JusPriceSmall;
        }

        private RechnungViewModel SetKundenListe()
        {
            RechnungViewModel model = new RechnungViewModel
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
