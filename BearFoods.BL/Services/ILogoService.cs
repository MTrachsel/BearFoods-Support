using Novacode;
using System.Xml.Linq;

namespace BearFoods.BL.Services
{
    public interface ILogoService
    {
        DocX Create(LogoData data);
    }
}
