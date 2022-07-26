using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public virtual DepartmentSemesterCourse Course { get; set; }
        public virtual Student Student { get; set; }
        public DateTime? Date { get; set; }
    }
}
