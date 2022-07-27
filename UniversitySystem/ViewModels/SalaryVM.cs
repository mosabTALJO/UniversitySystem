namespace UniversitySystem.ViewModels
{
    public class SalaryVM
    {
        public string Rank { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<ManagementStaffVM> ManagementStaffs { get; set; }
        public virtual ICollection<TeacherVM> Teachers { get; set; }
    }
}
