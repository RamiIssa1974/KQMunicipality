using AutoMapper;
using KQApi.Models;
using KQApi.Models.ProcessSharePoint.Entities;
using KQGeneral.Models.Registration;

namespace KQApi.Profiles
{
    public class RegistrationTypeProfile : Profile
    {
        public RegistrationTypeProfile()
        {
            CreateMap<SqlRegistrationType, RegistrationType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Type_name))
                .ForMember(dest => dest.IsActiv, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year));

            CreateMap<RegistrationTypeFields, RegistrationType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Type_name))
                .ForMember(dest => dest.IsActiv, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year));                 
        }
    }
}
