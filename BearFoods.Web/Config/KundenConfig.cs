using System.Collections.Generic;

namespace BearFoods.Web.Config
{
    public class KundenConfig
    {
        public List<Kunde> Kunden { get; set; }
        public int RechnungsNr { get; set; }
        public int LieferscheinNr { get; set; }
    }
}
