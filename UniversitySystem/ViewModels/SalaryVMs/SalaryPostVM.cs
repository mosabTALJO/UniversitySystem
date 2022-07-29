using UniversitySystem.Models;

namespace UniversitySystem.ViewModels.SalaryVMs
{
    public class SalaryPostVM
    {
        public string Rank { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int EmployeeId { get; set; }
    }
}
