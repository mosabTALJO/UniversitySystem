using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int TakenCourseId { get; set; }
        public virtual TakenCourse TakenCourse { get; set; }
        //public int DepSemCourseId { get; set; }
        //public int StudentId { get; set; }
        //public virtual DepartmentSemesterCourse DepSemCourse { get; set; }
        //public virtual Student Student { get; set; }
        public DateTime? Date { get; set; }
        public int AddedByEmployeeId { get; set; }
        //public virtual Employee AddedByEmployee { get; set; }
    }
}
