namespace UniversitySystem.Models
{
    public class ManagementStaff
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
