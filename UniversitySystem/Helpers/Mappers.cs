using AutoMapper;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<DepartmentSemesterCourse, DepartmentSemesterCourseVM>().ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course.Name)).ReverseMap();

            CreateMap<WeekProgram, WeekProgramVM>().ReverseMap();

            CreateMap<SemesterCourseTeacher, SemesterCourseTeacherVM>().ReverseMap();
        }
    }
}
