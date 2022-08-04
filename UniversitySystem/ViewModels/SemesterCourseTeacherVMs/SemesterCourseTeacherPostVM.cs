namespace UniversitySystem.ViewModels.SemesterCourseTeacherVMs
{
    public class SemesterCourseTeacherPostVM
    {
        public int DepSemCourseId { get; set; }
        public int TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxStudents { get; set; }
    }
}
