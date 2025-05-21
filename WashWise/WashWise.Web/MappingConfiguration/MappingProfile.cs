using AutoMapper;
using WashWise.Models;
using WashWise.Web.Models;

namespace WashWise.Web.MappingConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Building, BuildingFormViewModel>()
                .ReverseMap();
        }
    }
}