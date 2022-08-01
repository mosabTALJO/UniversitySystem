using UniversitySystem.Models;

namespace UniversitySystem.ViewModels.DepartmentSemesterCourseVM
{
    public class DepartmentSemesterCoursePostVM
    {
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int Credit { get; set; }
        public int StudentsAmount { get; set; }
        public int SemesterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
