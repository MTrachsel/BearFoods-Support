using BearFoods.Web.Config;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BearFoods.Web
{
    public class LieferscheinViewModel
    {
        [Display(Name = "Bestehender Kunde")]
        public string BestehenderKunde { get; set; }
        public List<SelectListItem> Kunden { get; set; }
        [Display(Name = "Kunden Name")]
        public string KundenName { get; set; }
        [Display(Name = "Kunden Nr")]
        public string KundeNr { get; set; }
        [Display(Name = "Liefer Nr")]
        public string LieferNr { get; set; }
        [Display(Name = "Adresszeile 1")]
        public string AdressZeile1 { get; set; }
        [Display(Name = "Adresszeile 2")]
        public string AdressZeile2 { get; set; }
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
