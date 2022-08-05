namespace UniversitySystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserTypes UserType { get; set; }
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
