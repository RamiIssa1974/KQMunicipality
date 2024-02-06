using AutoMapper;
using KQApi.Models;
using KQApi.Models.ProcessSharePoint.Entities;
using KQGeneral.Models.Registration;

namespace KQApi.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<SqlTeacher, Teacher>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.TeacherId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TName))
                .ForMember(dest => dest.MaximumCapacity, opt => opt.MapFrom(src => src.MaxNumber))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Active.ToLower() == "yes"))
                .ForMember(dest => dest.MithamId, opt => opt.MapFrom(src => src.MithamId));

            CreateMap<TeacherFields, Teacher>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.TeacherId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TName))
                .ForMember(dest => dest.MaximumCapacity, opt => opt.MapFrom(src => src.MaxNumber))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Active.ToLower() == "yes"))
                .ForMember(dest => dest.MithamId, opt => opt.MapFrom(src => src.MithamId));
        }

    }
}
