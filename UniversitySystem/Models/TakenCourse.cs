namespace UniversitySystem.Models
{
    public class TakenCourse : TrackableEntity
    {
        public int Id { get; set; }
        public int DepSemCourseId { get; set; }
        public int DepStudentId { get; set; }
        public int? SemCourseTeacherId { get; set; }
        public virtual DepartmentSemesterCourse DepSemCourse { get; set; }
        //public virtual DepartmentStudent DepStudent { get; set; }
        public virtual SemesterCourseTeacher? SemCourseTeacher { get; set; }
        public double? Grade { get; set; }
        public virtual ICollection<Attendance>? Attendances { get; set; }
        public bool IsApproved { get; set; }
    }
}
