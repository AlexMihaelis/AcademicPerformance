using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicPerformance.Models.DTO
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int DisciplineId { get; set; }
        public string? Value { get; set; }
        public DateTime Date { get; set; }
        public Student Student { get; set; }
    }
}
