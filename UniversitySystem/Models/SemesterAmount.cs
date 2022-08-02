namespace UniversitySystem.Models
{
    public class SemesterAmount
    {
        public int Id { get; set; }
        public int SemId { get; set; }  
        public int DepId { get; set; }
        public virtual Semester Sem { get; set; }
        public virtual Department Dep { get; set; }
        public double Price { get; set; }
    }
}
