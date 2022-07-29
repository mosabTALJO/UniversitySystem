using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int DepSemCourseId { get; set; }
        public int StudentId { get; set; }
        public virtual DepartmentSemesterCourse DepSemCourse { get; set; }
        public virtual Student Student { get; set; }
        public DateTime? Date { get; set; }
    }
}
