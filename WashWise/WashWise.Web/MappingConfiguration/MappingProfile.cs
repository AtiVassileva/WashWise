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

            CreateMap<WashingMachine, WashingMachineAvailabilityViewModel>()
                .ForMember(dest => dest.WashingMachineId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MachineModel, opt => opt.MapFrom(src => src.Model));

            CreateMap<Reservation, MyReservationViewModel>()
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WashingMachineId, opt => opt.MapFrom(src => src.WashingMachineId))
                .ForMember(dest => dest.WashingMachineModel, opt => opt.MapFrom(src => src.WashingMachine!.Model))
                .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(src => src.WashingMachine!.Building.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.WashingMachine!.Building.Address))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status!.Name))
                .ForMember(dest => dest.CanCancel, opt => opt.MapFrom(src =>
                    src.StartTime > DateTime.Now && src.Status!.Name != "Канселирана"));

            CreateMap<ReportInputModel, Report>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.WashingMachineId, opt => opt.MapFrom(src => src.WashingMachineId))
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedOn, opt => opt.Ignore())
                .ForMember(dest => dest.VersionNo, opt => opt.Ignore())
                .ForMember(dest => dest.GeneratedAt, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorId, opt => opt.Ignore())
                .ForMember(dest => dest.IsResolved, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Reservation, ReservationListViewModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User!.Email))
                .ForMember(dest => dest.Machine, opt => opt.MapFrom(src => src.WashingMachine!.Model))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status!.Name))
                .ReverseMap();
        }
    }
}