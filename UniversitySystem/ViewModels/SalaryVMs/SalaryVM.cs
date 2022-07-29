using UniversitySystem.ViewModels.SemesterCourseTeacherVMs;

namespace UniversitySystem.ViewModels.SalaryVMs
{
    public class SalaryVM
    {
        public string Rank { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeId { get; set; }
    }
}
