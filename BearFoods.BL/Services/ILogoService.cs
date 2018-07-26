using Novacode;
using System.Xml.Linq;

namespace BearFoods.BL.Services
{
    public interface ILogoService
    {
        XDocument Create(LogoData data);
    }
}
