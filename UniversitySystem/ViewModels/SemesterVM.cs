namespace UniversitySystem.ViewModels
{
    public class SemesterVM
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string GuzBaharYaz { get; set; }
        public DateTime CourseTakingStartDate { get; set; }
        public DateTime CourseTakingEndDate { get; set; }
        public int AddedByEmployeeId { get; set; }
    }
}