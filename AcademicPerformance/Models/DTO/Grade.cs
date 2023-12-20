using System.ComponentModel.DataAnnotations;

namespace AcademicPerformance.Models.DTO
{
    public class Grade
    {
        [Key]
        public int GradesID { get; set; }
        public int StudentsID { get; set; }
        public int DisciplinesID { get; set; }
        public string? Grades { get; set; }
        public DateTime Date { get; set; }
        public Student Student { get; set; }
    }
}
