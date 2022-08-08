namespace UniversitySystem.Models
{
    public class WeekProgram : TrackableEntity
    {
        public int Id { get; set; }
        public int DepSemCourseId { get; set; }
        public virtual DepartmentSemesterCourse DepSemCourse { get; set; }
        public string Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
