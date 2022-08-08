using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackableEntities.Common.Core;
using UniversitySystem.Helpers;
using UniversitySystem.Models;

namespace UniversitySystem.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        void UpdateAuditableFields()
        {
            var entries = ChangeTracker.Entries();
            var now = DateTime.UtcNow;
            var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Anonymous" ;
            
            foreach(var entry in entries)
            { 
                if (entry.Entity is Helpers.ITrackable entity)
                {
                    if(entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = user;
                        entity.CreatedAt = now;
                        entity.UpdatedBy = user;
                        entity.UpdatedAt = now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        if (entity.UpdateCreatedAt)
                            entity.CreatedAt = now;
                        entity.UpdatedBy = user;
                        entity.UpdatedAt = now;
                    }
                    else if(entry.State == EntityState.Deleted && entry.Entity is ISoftDelete sd)
                    {
                        if (!sd.CompleteRemove)
                        {
                            entry.State = EntityState.Modified;
                            entity.UpdatedBy = user;
                            sd.DeletedAt = now;
                            sd.DeletedBy = user;
                        }
                    }
                }
            }
        
        }
        public override int SaveChanges()
        {
            UpdateAuditableFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateAuditableFields();
            return base.SaveChangesAsync(cancellationToken);
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
