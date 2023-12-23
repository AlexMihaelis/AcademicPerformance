using System.ComponentModel.DataAnnotations;

namespace AcademicPerformance.Models.DTO
{
    public class Faculty
    {
        [Key]
        public int FacultyId { get; set; }
        public string Name { get; set; }
    }
}
