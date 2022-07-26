namespace UniversitySystem.Models
{
    public class SemesterCourseTeacher
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public virtual DepartmentSemesterCourse Course { get; set; }
        public virtual Teacher Teacher { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public virtual ICollection<TakenCourse> TakenCourses { get; set; }
    }
}
