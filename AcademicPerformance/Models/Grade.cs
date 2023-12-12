namespace AcademicPerformance.Models
{
    public class Grade
    {
        public int GradesID { get; set; }
        public int StudentsID { get; set; }
        public int DisciplinesID { get; set; }
        public int? Grades { get; set; }
        public DateOnly Date { get; set; }
    }
}
