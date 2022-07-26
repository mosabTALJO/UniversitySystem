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

            CreateMap<DepartmentStudent, DepartmentStudentVM>().ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.FirstName + " " + src.Supervisor.LastName))
                                                               .ForMember(dest => dest.StudentFirstName , opt => opt.MapFrom(src => src.Student.FirstName)).ReverseMap();

            CreateMap<Course, CourseVM>().ReverseMap();
        }
    }
}
