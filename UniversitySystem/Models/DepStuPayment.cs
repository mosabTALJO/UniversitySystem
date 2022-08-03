namespace UniversitySystem.Models
{
    public class DepStuPayment
    {
        public int Id { get; set; }
        public int DepStuId { get; set; }
        public int SemAmntID { get; set; }
        public virtual DepartmentStudent DepStu { get; set; }
        //public virtual SemesterAmount SemAmnt { get; set; }
        public double Total { get; set; }
        public bool IsPayed { get; set; }
    }
}
