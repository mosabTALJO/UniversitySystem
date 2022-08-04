namespace UniversitySystem.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string GuzBaharYaz { get; set; }
        public DateTime CourseTakingStartDate { get; set; }
        public DateTime CourseTakingEndDate { get; set; }
        public virtual ICollection<DepartmentSemesterCourse> DepSemCourses { get; set; }    
        public virtual ICollection<SemesterAmount> SemesterAmounts { get; set; }
    }
}
