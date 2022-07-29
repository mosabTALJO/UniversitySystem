namespace UniversitySystem.ViewModels
{
    public class SemesterCourseTeacherVM
    {
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        //public string Course { get; set; }
        //public string Teacher { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
