using System.ComponentModel.DataAnnotations;

namespace AcademicPerformance.Models.DTO
{
    public class Faculty
    {
        [Key]
        public int FacultiesID { get; set; }
        public string NameFaculty { get; set; }
    }
}
