namespace UniversitySystem.ViewModels.DepartmentStudentVMs
{
    public class DepartmentStudentPostVM
    {
        public int DepartmentId { get; set; }
        public int StudentId { get; set; }
        public int SupervisorId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime? GraduationDate { get; set; }
        public bool IsActive { get; set; }
        public int Year { get; set; }
        public int AddedByEmployeeId { get; set; }
    }
}
