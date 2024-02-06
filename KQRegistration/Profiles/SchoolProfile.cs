using AutoMapper;
using KQApi.Models;
using KQApi.Models.ProcessSharePoint.Entities;
using KQGeneral.Models.Registration;

namespace KQApi.Profiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<SchoolFields, School>()
                .ForMember(dest => dest.MaximumCapacity, 
                opt => opt.MapFrom(src => src.MaxNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title));

            CreateMap<SqlSchool, School>()
               .ForMember(dest => dest.MaximumCapacity,
               opt => opt.MapFrom(src => src.MaxNumber))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title));
        }
        
    }
}
