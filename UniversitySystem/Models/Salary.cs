namespace UniversitySystem.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public string Rank { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public virtual ManagementStaff Empolyee { get; set; }
        public virtual ICollection<ManagementStaff> ManagementStaffs { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
