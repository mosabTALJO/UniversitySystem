using AutoMapper;
using System.Linq;
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

            CreateMap<SemesterCourseTeacher, SemesterCourseTeacherVM>()/*.ForMember(dest => dest.Course , opt => opt.MapFrom(src => src.Course.Course.Name))
                                                                       .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher.Employee.FirstName))*/.ReverseMap();

            CreateMap<DepartmentStudent, DepartmentStudentVM>().ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.FirstName + " " + src.Supervisor.LastName))
                                                               .ForMember(dest => dest.StudentFirstName , opt => opt.MapFrom(src => src.Student.FirstName)).ReverseMap();

            CreateMap<Course, CourseVM>().ReverseMap();

            CreateMap<Employee, EmployeeVM>().ReverseMap();

            CreateMap<Salary, SalaryVM>().ReverseMap();

            CreateMap<Teacher, TeacherVM>().ForMember(dest => dest.SemesterCourseTeachers , opt => opt.MapFrom(src => src.SemesterCourseTeachers)).ReverseMap(); // we may have some problems here 

            CreateMap<Student, StudentVM>().ReverseMap(); // and here " baisecly i fucked up "
        
            CreateMap<ManagementStaff, ManagementStaffVM>().ReverseMap();
        
            CreateMap<Department , DepartmentVM>().ReverseMap();

            CreateMap<Employee, EmployeeInfoVM>().ReverseMap();


        }
    }
}
