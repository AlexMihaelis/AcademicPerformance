using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademicPerformance.Models.DTO
{
    public class Discipline
    {
        [Key]
        public int DisciplineId { get; set; }
        public string Name { get; set; }
    }
}
