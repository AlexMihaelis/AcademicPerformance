using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicPerformance.Models.DTO
{
    [Table("Groups")]
    /// <summary>
    /// Модель группы
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Идентификатор группы
        /// </summary>
        [Key]
        public int GroupId { get; set; }

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string Name { get; set; }
        public int FacultyId { get; set; }
    }
}
