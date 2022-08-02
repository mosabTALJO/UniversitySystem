namespace UniversitySystem.Models
{
    public class DepartmentSemesterCourse
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Course Course { get; set; }
        public virtual Semester Semester { get; set; }
        public int Credit { get; set; }
        public int MaxStudents { get; set; }
        public int NumberOfStudents { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<TakenCourse>? TakenCourses { get; set; }
        public virtual ICollection<WeekProgram>? WeekPrograms { get; set; }
        public virtual ICollection<SemesterCourseTeacher>? SemesterCourseTeachers { get; set; }
        //public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
