namespace UniversitySystem.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public virtual ICollection<DepartmentStudentVM> SupervisedStudents { get; set; }
    }
}
