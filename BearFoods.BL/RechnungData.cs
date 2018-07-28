using System;
using System.ComponentModel.DataAnnotations;

namespace BearFoods.BL
{
    public class RechnungData
    {
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
        public decimal EinzelpreisBBQ { get; set; }
        public decimal EinzelpreisPizza { get; set; }
        public decimal EinzelpreisJus { get; set; }
        public decimal TotalBBQ { get; set; }
        public decimal TotalPizza { get; set; }
        public decimal TotalJus { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
