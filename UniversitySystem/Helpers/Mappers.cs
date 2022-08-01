using AutoMapper;
using System.Linq;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;
using UniversitySystem.ViewModels.DepartmentSemesterCourseVM;
using UniversitySystem.ViewModels.EmployeeVM;
using UniversitySystem.ViewModels.SalaryVMs;
using UniversitySystem.ViewModels.SemesterCourseTeacherVMs;
using UniversitySystem.ViewModels.TakenCourseVMs;

namespace UniversitySystem.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<DepartmentSemesterCourse, DepartmentSemesterCourseVM>().ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course.Name)).ReverseMap();
            CreateMap<DepartmentSemesterCourse, DepartmentSemesterCoursePostVM>().ReverseMap();

            CreateMap<WeekProgram, WeekProgramVM>().ReverseMap();

            CreateMap<SemesterCourseTeacher, SemesterCourseTeacherVM>().ReverseMap();

            CreateMap<DepartmentStudent, DepartmentStudentVM>().ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.FirstName + " " + src.Supervisor.LastName))
                                                               .ForMember(dest => dest.StudentFirstName , opt => opt.MapFrom(src => src.Student.FirstName)).ReverseMap();

            CreateMap<Course, CourseVM>().ReverseMap();

            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<Employee, EmployeeInfoVM>().ReverseMap();

            CreateMap<Salary, SalaryVM>().ReverseMap();
            CreateMap<Salary, SalaryPostVM>().ReverseMap();

            CreateMap<Teacher, TeacherVM>().ForMember(dest => dest.SemesterCourseTeachers , opt => opt.MapFrom(src => src.SemesterCourseTeachers)).ReverseMap(); // we may have some problems here 

            CreateMap<Student, StudentVM>().ReverseMap(); // and here " baisecly i fucked up "
        
            CreateMap<ManagementStaff, ManagementStaffVM>().ReverseMap();
        
            CreateMap<Department , DepartmentVM>().ReverseMap();

            CreateMap<TakenCourse, TakenCourseVM>().ReverseMap();


        }
    }
}
