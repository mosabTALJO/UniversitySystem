using Microsoft.AspNetCore.Identity;

namespace UniversitySystem.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Address { get; set; }
        //public UserTypes UserType { get; set; }
        //public virtual DepartmentStudent? DepartmentStudent { get; set; }
        //public virtual Employee? Employee { get; set; }
    }

    public enum UserTypes
    {
        Manager,
        Teacher,
        Student
    }
}
