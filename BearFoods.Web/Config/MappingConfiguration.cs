using AutoMapper;
using BearFoods.BL;
using BearFoods.Web;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LieferscheinViewModel, LieferscheinData>();
        CreateMap<RechnungViewModel, RechnungData>();
    }
}