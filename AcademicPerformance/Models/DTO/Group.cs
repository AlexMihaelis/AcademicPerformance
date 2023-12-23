using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicPerformance.Models.DTO
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int FacultyId { get; set; }
    }
}
