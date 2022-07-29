namespace UniversitySystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<SemesterCourseTeacher> SemesterCourseTeachers { get; set; }
    }
}
