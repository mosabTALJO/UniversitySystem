namespace UniversitySystem.Models
{
    public class TakenCourse
    {
        public int Id { get; set; }
        public int DepSemCourseId { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public virtual DepartmentSemesterCourse DepSemCourse { get; set; }
        //public virtual DepartmentStudent Student { get; set; }
        public virtual Teacher Teacher { get; set; }
        public double Grade { get; set; }
    }
}
