namespace UniversitySystem.ViewModels
{
    public class TeacherVM
    {
        public string Name { get; set; }    
        public string Department { get; set; }
        public int Salary { get; set; }
        public virtual ICollection<SemesterCourseTeacherVM> SemesterCourseTeachers { get; set; }
    }
}