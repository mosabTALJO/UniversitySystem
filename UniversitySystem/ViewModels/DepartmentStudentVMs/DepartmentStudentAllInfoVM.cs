namespace UniversitySystem.ViewModels.DepartmentStudentVMs
{
    public class DepartmentStudentAllInfoVM
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime? GraduationDate { get; set; }
        public bool IsActive { get; set; }
        public int Year { get; set; }
        public double Balance { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
    }
}
