namespace UniversitySystem.ViewModels.WeekProgramVMs
{
    public class WeekProgramPostVM
    {
        public int Id { get; set; }
        public int DepSemCourseId { get; set; }        
        public string Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
