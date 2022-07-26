namespace UniversitySystem.Models
{
    public class ManagementStaff
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SalaryId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Salary Salary { get; set; }
    }
}
