using UniversitySystem.ViewModels.SemesterCourseTeacherVMs;

namespace UniversitySystem.ViewModels.DepartmentSemesterCourseVM
{
    public class DepartmentSemesterCourseVM
    {
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }
        public string Course { get; set; }
        public int Credit { get; set; }
        public int StudentsAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<WeekProgramVM> WeekPrograms { get; set; }
        public virtual ICollection<SemesterCourseTeacherVM> SemesterCourseTeachers { get; set; }
    }
}
