namespace UniversitySystem.Models
{
    public class Salary : SoftDeleteEntity
    {
        public int Id { get; set; }
        public string Rank { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
