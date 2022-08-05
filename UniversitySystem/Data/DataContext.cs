using Microsoft.EntityFrameworkCore;
using UniversitySystem.Models;

namespace UniversitySystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //}
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentSemesterCourse> DepartmentSemesterCourses { get; set; }
        public DbSet<DepartmentStudent> DepartmentStudents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ManagementStaff> ManagementStaffs { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<SemesterCourseTeacher> SemesterCourseTeachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TakenCourse> TakenCourses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<WeekProgram> WeekPrograms { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<DepStuPayment> DepStuPayments { get; set; }
        public DbSet<SemesterAmount> SemesterAmounts { get; set; }
        //public DbSet<User> Users { get; set; }
    }
}
