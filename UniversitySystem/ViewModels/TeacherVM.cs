namespace UniversitySystem.ViewModels.SemesterCourseTeacherVMs
{
    public class TeacherVM
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Department { get; set; }
        public virtual ICollection<SemesterCourseTeacherVM> SemesterCourseTeachers { get; set; }
    }
}