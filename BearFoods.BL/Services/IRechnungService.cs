using Novacode;

namespace BearFoods.BL.Services
{
    public interface IRechnungService
    {
        DocX Create(RechnungData data);
    }
}
