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
        public double Balance { get; set; }
        public virtual ICollection<TakenCourse> TakenCourses { get; set; }
        public virtual ICollection<DepStuPayment> DepStuPayments { get; set; }
        public int AddedByEmployeeId { get; set; }
        //public int UserId => Id;
        //public virtual User? User { get; set; }
        //public virtual Employee AddedByEmployee { get; set; }
        //public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
