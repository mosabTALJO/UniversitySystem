namespace UniversitySystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DepartmentSemesterCourse> Semesters { get; set; }
        public virtual ICollection<DepartmentStudent> Students { get; set; }
    }
}
