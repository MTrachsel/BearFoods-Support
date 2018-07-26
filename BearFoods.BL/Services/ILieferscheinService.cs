using Novacode;

namespace BearFoods.BL.Services
{
    public interface ILieferscheinService
    {
        DocX Create(LieferscheinData data);
    }
}
