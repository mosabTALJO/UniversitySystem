using UniversitySystem.ViewModels.DepartmentStudentVMs;

namespace UniversitySystem.ViewModels.StudentVMs
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public List<DepartmentStudentWithStuVM> DepStus { get; set; }

    }
}
