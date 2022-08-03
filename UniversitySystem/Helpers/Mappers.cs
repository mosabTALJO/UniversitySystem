using AutoMapper;
using System.Linq;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;
using UniversitySystem.ViewModels.DepartmentSemesterCourseVM;
using UniversitySystem.ViewModels.DepartmentStudentVMs;
using UniversitySystem.ViewModels.EmployeeVM;
using UniversitySystem.ViewModels.SalaryVMs;
using UniversitySystem.ViewModels.SemesterCourseTeacherVMs;
using UniversitySystem.ViewModels.StudentVMs;
using UniversitySystem.ViewModels.TakenCourseVMs;
using UniversitySystem.ViewModels.WeekProgramVMs;

namespace UniversitySystem.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<DepartmentSemesterCourse, DepartmentSemesterCourseVM>().ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course.Name)).ReverseMap();
            CreateMap<DepartmentSemesterCourse, DepartmentSemesterCoursePostVM>().ReverseMap();

            CreateMap<WeekProgram, WeekProgramPostVM>().ForMember(dest => dest.DepSemCourseId, opt => opt.MapFrom(src => src.DepSemCourseId)).ReverseMap();
            CreateMap<WeekProgram, WeekProgramVM>().ForMember(dest => dest.DepSemCourseId, opt => opt.MapFrom(src => src.DepSemCourseId))
                                                   .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.DepSemCourse.Course.Name)).ReverseMap();

            CreateMap<SemesterCourseTeacher, SemesterCourseTeacherVM>().ReverseMap();

            CreateMap<DepartmentStudent, DepartmentStudentVM>().ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.FirstName + " " + src.Supervisor.LastName))
                                                               .ForMember(dest => dest.StudentFirstName , opt => opt.MapFrom(src => src.Student.FirstName)).ReverseMap();
            CreateMap<DepartmentStudent, DepartmentStudentPostVM>().ReverseMap();
            CreateMap<DepartmentStudent, DepartmentStudentWithStuVM>().ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.FirstName + " " + src.Supervisor.LastName));

            CreateMap<Course, CourseVM>().ReverseMap();

            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<Employee, EmployeeInfoVM>().ReverseMap();

            CreateMap<Salary, SalaryVM>().ReverseMap();
            CreateMap<Salary, SalaryPostVM>().ReverseMap();

            CreateMap<Teacher, TeacherVM>().ForMember(dest => dest.SemesterCourseTeachers , opt => opt.MapFrom(src => src.SemesterCourseTeachers)).ReverseMap(); // we may have some problems here 

            CreateMap<Student, StudentVM>().ForMember(dest => dest.DepStus, opt => opt.MapFrom(src => src.DepartmentStudents)).ReverseMap(); 
            CreateMap<Student, StudentPostVM>().ReverseMap(); 
        
            CreateMap<ManagementStaff, ManagementStaffVM>().ReverseMap();
        
            CreateMap<Department , DepartmentVM>().ReverseMap();

            CreateMap<TakenCourse, TakenCourseVM>().ReverseMap();

            CreateMap<Semester, SemesterVM>().ReverseMap();

            CreateMap<DepStuPayment, DepStuPaymentVM>().ReverseMap();

            CreateMap<SemesterAmount , SemesterAmountVM>().ReverseMap();
        }
    }
}
