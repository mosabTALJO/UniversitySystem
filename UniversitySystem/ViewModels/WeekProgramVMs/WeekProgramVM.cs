namespace UniversitySystem.ViewModels.WeekProgramVMs
{
    public class WeekProgramVM
    {
        public int DepSemCourseId { get; set; }
        public string CourseName { get; set; }
        public string Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
