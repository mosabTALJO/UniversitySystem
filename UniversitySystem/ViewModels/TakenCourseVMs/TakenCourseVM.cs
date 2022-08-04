namespace UniversitySystem.ViewModels.TakenCourseVMs
{
    public class TakenCourseVM
    {
        public int Id { get; set; }
        public int DepSemCourseId { get; set; }
        public int DepStudentId { get; set; }
        public int SemCourseTeacherId { get; set; }
        public double Grade { get; set; }
    }
}
