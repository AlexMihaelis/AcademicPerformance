using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademicPerformance.Models.DTO
{
    public class Discipline
    {
        [Key]
        public int DisciplinesID { get; set; }
        public string NameDiscipline { get; set; }
    }
}
