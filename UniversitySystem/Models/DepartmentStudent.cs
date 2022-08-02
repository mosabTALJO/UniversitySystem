namespace UniversitySystem.Models
{
    public class DepartmentStudent
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int StudentId { get; set; }
        public int SupervisorId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Student Student { get; set; }
        public virtual Employee Supervisor { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime? GraduationDate { get; set; }
        public bool IsActive { get; set; }
        public int Year { get; set; }
        public virtual ICollection<TakenCourse> TakenCourses { get; set; }
        //public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
