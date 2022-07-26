namespace UniversitySystem.Models
{
    public class WeekProgram
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public virtual DepartmentSemesterCourse Course { get; set; }
        public string Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
