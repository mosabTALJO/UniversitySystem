namespace UniversitySystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DepartmentSemesterCourse> DepartmentSemesterCourses { get; set; }
        public int AddedByEmployeeId { get; set; }
        //public virtual Employee AddedByEmployee { get; set; }
    }
}
