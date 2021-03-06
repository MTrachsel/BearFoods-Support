﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BearFoods.Web
{
    public class RechnungViewModel
    {
        [Display(Name = "Bestehender Kunde")]
        public string BestehenderKunde { get; set; }
        public List<SelectListItem> Kunden { get; set; }
        [Display(Name = "Kunden Name")]
        public string Kunde { get; set; }
        [Display(Name = "Adresszeile 1")]
        public string AdressZeile1 { get; set; }
        [Display(Name = "Adresszeile 2")]
        public string AdressZeile2 { get; set; }
        public string Jahr { get { return DateTime.Today.Year.ToString(); } }
        [Display(Name = "Rechnungsnummer")]
        public string RechnungsNummer { get; set; }
        [Display(Name = "Rechnungsdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RechnungsDatum { get; set; }
        [Display(Name = "Lieferdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LieferDatum { get; set; }
        [Display(Name = "Bär-BQ Sauce")]
        public int MengeBBQ { get; set; }
        [Display(Name = "Pizza Sauce")]
        public int MengePizza { get; set; }
        [Display(Name = "Braten Sauce")]
        public int MengeJus { get; set; }
        [Display(Name = "Bär-BQ Sauce klein")]
        public int MengeBBQSmall { get; set; }
        [Display(Name = "Braten Sauce klein")]
        public int MengeJusSmall { get; set; }        
    }
}
