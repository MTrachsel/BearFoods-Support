namespace BearFoods.BL.Services
{
    public interface ICalculateService
    {
        RechnungData CalulateRechnungTotals(RechnungData data);
        LieferscheinData CalulateLieferscheinTotals(LieferscheinData data);
    }
}
