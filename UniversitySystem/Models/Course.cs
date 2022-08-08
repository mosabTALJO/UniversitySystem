namespace UniversitySystem.Models
{
    public class Course : SoftDeleteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DepartmentSemesterCourse> DepartmentSemesterCourses { get; set; }
    }
}
