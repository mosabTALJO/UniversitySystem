namespace UniversitySystem.ViewModels
{
    public class AttendanceVM
    {
        public int Id { get; set; }
        public int TakenCourseId { get; set; }
        public DateTime? Date { get; set; }
        public int AddedByEmployeeId { get; set; }
    }
}
