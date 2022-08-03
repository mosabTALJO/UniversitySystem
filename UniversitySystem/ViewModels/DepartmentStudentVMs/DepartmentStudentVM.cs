namespace UniversitySystem.ViewModels.DepartmentStudentVMs
{
    public class DepartmentStudentVM
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int StudentId { get; set; }
        public int SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public string StudentFirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime? GraduationDate { get; set; }
        public bool IsActive { get; set; }
        public int Year { get; set; }
        public double Balance { get; set; }
        //public virtual ICollection<TakenCourse> TakenCourses { get; set; }
        //public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
