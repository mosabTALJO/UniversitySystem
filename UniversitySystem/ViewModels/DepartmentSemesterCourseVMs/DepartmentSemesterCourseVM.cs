using UniversitySystem.ViewModels.SemesterCourseTeacherVMs;
using UniversitySystem.ViewModels.WeekProgramVMs;

namespace UniversitySystem.ViewModels.DepartmentSemesterCourseVM
{
    public class DepartmentSemesterCourseVM
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }
        public string Course { get; set; }
        public int Credit { get; set; }
        public int StudentsAmount { get; set; }
        public int SemesterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<WeekProgramVM> WeekPrograms { get; set; }
        public virtual ICollection<SemesterCourseTeacherVM> SemesterCourseTeachers { get; set; }
    }
}
