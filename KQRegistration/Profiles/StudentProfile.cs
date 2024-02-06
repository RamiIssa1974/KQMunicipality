using AutoMapper;
using KQApi.Models;
using KQApi.Models.ProcessSharePoint.Entities;
using KQGeneral.Models.Registration;

namespace KQApi.Profiles
{
    public class StudentProfile:Profile
    {
        public StudentProfile()
        {
            CreateMap<SqlStudent, Student>()
                .ForMember(dest => dest.IDN, opt => opt.MapFrom(src => src.IDNo))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher))
                .ForMember(dest => dest.Agree, opt => opt.MapFrom(src => src.Agree == "Yes"))
                .ForMember(dest => dest.Confirm, opt => opt.MapFrom(src => src.Confirm == "Yes"))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Fphone) ? src.Fphone : src.Mphone))
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School))
                .ForMember(dest => dest.FirstAlternativeSchool, opt => opt.MapFrom(src => src.AltSchool1))
                .ForMember(dest => dest.SecondAlternativeSchool, opt => opt.MapFrom(src => src.AltSchool2))
                .ForMember(dest => dest.FirstAlternativeTeacher, opt => opt.MapFrom(src => src.AltTeacher1))
                .ForMember(dest => dest.SecondAlternativeTeacher, opt => opt.MapFrom(src => src.AltTeacher2));

            CreateMap<StudentFields, SqlStudent>()
               .ForMember(dest => dest.IDNo, opt => opt.MapFrom(src => src.IDNo))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher))
               .ForMember(dest => dest.Agree, opt => opt.MapFrom(src => src.Agree == "Yes"))
               .ForMember(dest => dest.Confirm, opt => opt.MapFrom(src => src.Confirm == "Yes"))
               .ForMember(dest => dest.Fphone, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Fphone) ? src.Fphone : src.Mphone))
               .ForMember(dest => dest.School, opt => opt.MapFrom(src => src.School))
               .ForMember(dest => dest.AltSchool1, opt => opt.MapFrom(src => src.AltSchool1))
               .ForMember(dest => dest.AltSchool2, opt => opt.MapFrom(src => src.AltSchool2))
               .ForMember(dest => dest.AltTeacher1, opt => opt.MapFrom(src => src.AltTeacher1))
               .ForMember(dest => dest.AltTeacher2, opt => opt.MapFrom(src => src.AltTeacher2));


            CreateMap<StudentFields, Student>()
                .ForMember(dest => dest.IDN, opt => opt.MapFrom(src => src.IDNo))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher))
                .ForMember(dest => dest.Agree, opt => opt.MapFrom(src => src.Agree == "Yes"))
                .ForMember(dest => dest.Confirm, opt => opt.MapFrom(src => src.Confirm == "Yes"))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Fphone)  ? src.Fphone: src.Mphone))
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School))
                .ForMember(dest => dest.FirstAlternativeSchool, opt => opt.MapFrom(src => src.AltSchool1))
                .ForMember(dest => dest.SecondAlternativeSchool, opt => opt.MapFrom(src => src.AltSchool2))
                .ForMember(dest => dest.FirstAlternativeTeacher, opt => opt.MapFrom(src => src.AltTeacher1))
                .ForMember(dest => dest.SecondAlternativeTeacher, opt => opt.MapFrom(src => src.AltTeacher2));

           
        }        
    }
}
