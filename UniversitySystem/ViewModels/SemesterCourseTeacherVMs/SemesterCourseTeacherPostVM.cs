namespace UniversitySystem.ViewModels.SemesterCourseTeacherVMs
{
    public class SemesterCourseTeacherPostVM
    {
        // this has the same properties as SemesterCourseTeacherVM, but it has different mapping
        public int DepSemCourseId { get; set; }
        public int TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
