namespace UniversitySystem.ViewModels
{
    public class AttendanceVM
    {
        public int Id { get; set; }
        public int DepSemCourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime? Date { get; set; }
    }
}
