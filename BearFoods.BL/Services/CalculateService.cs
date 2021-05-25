using System;

namespace BearFoods.BL.Services
{
    public class CalculateService : ICalculateService
    {
        public LieferscheinData CalulateLieferscheinTotals(LieferscheinData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            data.TotalBBQ = data.EinzelpreisBBQ * data.MengeBBQ;
            data.TotalPizza = data.EinzelpreisPizza * data.MengePizza;
            data.TotalJus = data.EinzelpreisJus * data.MengeJus;

            data.Total = data.TotalBBQ + data.TotalPizza + data.TotalJus + data.TotalBBQSmall + data.TotalJusSmall;

            return data;
        }

        public RechnungData CalulateRechnungTotals(RechnungData data)
        {
            const int DELIVERY_COST = 4;
            if (data == null) throw new ArgumentNullException(nameof(data));

            data.TotalBBQ = data.EinzelpreisBBQ * data.MengeBBQ;
            data.TotalBBQSmall = data.EinzelpreisBBQSmall * data.MengeBBQSmall;
            data.TotalPizza = data.EinzelpreisPizza * data.MengePizza;
            data.TotalJus = data.EinzelpreisJus * data.MengeJus;
            data.TotalJusSmall = data.EinzelpreisJusSmall * data.MengeJusSmall;

            data.Total = data.TotalBBQ + data.TotalPizza + data.TotalJus + data.TotalBBQSmall + data.TotalJusSmall + DELIVERY_COST;
            data.SubTotal = data.Total - DELIVERY_COST;

            return data;
        }
    }
}
