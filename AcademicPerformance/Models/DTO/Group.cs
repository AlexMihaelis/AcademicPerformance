using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicPerformance.Models.DTO
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        public int GroupsID { get; set; }
        public string NameGroups { get; set; }
        public int FacultiesID { get; set; }
    }
}
