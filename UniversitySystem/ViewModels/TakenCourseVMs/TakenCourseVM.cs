namespace UniversitySystem.ViewModels.TakenCourseVMs
{
    public class TakenCourseVM
    {
        public int Id { get; set; }
        public int DepSemCourseId { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public double Grade { get; set; }
    }
}
