namespace BearFoods.Web
{
    public class RechnungViewModel
    {
        public string Kunde { get; set; }
        public string AdressZeile1 { get; set; }
        public string AdressZeile2 { get; set; }
        public string Jahr { get; set; }
        public string RechnungsNummer { get; set; }
        public string RechnungsDatum { get; set; }
        public string LieferDatum { get; set; }
        public string MengeBBQ { get; set; }
        public string MengePizza { get; set; }
        public string MengeJus { get; set; }
        public string EinzelpreisBBQ { get; set; }
        public string EinzelpreisPizza { get; set; }
        public string EinzelpreisJus { get; set; }
        public string TotalBBQ { get; set; }
        public string TotalPizza { get; set; }
        public string TotalJus { get; set; }
        public string SubTotal { get; set; }
        public string Total{ get; set; }
    }
}
