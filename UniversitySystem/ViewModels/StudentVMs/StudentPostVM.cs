namespace UniversitySystem.ViewModels.StudentVMs
{
    public class StudentPostVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public int AddedByEmployeeId { get; set; }
    }
}
