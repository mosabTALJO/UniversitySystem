namespace UniversitySystem.Models
{
    public class SemesterCourseTeacher
    {
        public int Id { get; set; }
        public int DepSemCourseId { get; set; }
        public int TeacherId { get; set; }
        public virtual DepartmentSemesterCourse DepSemCourse { get; set; }
        public virtual Teacher Teacher { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int NumberOfStudents { get; set; }
        public int MaxStudents { get; set; }
        //public virtual ICollection<TakenCourse> TakenCourses { get; set; }
        public int AddedByEmployeeId { get; set; }
       // public virtual Employee AddedByEmployee { get; set; }
    }
}
