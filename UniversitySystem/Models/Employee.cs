using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        //public string Email { get; set; }
        public long PhoneNumber { get; set; }
        //public int? CurrentSalaryId { get; set; }
        //[NotMapped]
        //public virtual Salary? CurrentSalary { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<ManagementStaff> ManagementStaffs { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<DepartmentStudent> DepartmentStudents { get; set; }
        public int AddedByEmployeeId { get; set; }
        //public int UserId { get; set; }
        //public virtual User? User { get; set; }
        //public virtual Employee AddedByEmployee { get; set; }
    }
}
