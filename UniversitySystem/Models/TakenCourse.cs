namespace UniversitySystem.Models
{
    public class TakenCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public virtual DepartmentSemesterCourse Course { get; set; }
        //public virtual DepartmentStudent Student { get; set; }
        public virtual Teacher Teacher { get; set; }
        public double Grade { get; set; }
    }
}
