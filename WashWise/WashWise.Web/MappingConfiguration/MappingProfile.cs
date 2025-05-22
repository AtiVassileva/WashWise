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
            CreateMap<WashingMachine, WashingMachineViewModel>()
                .ForMember(dest => dest.BuildingAddress, opt => opt.MapFrom(src => string.Concat(src.Building.Name, " - ", src.Building.Address)))
                .ForMember(dest => dest.ConditionName, opt => opt.MapFrom(src => src.Condition!.Name));

            CreateMap<WashingMachine, WashingMachineFormModel>()
                .ForMember(dest => dest.MachineModel, opt => opt.MapFrom(src => src.Model))
                .ReverseMap();
        }
    }
}