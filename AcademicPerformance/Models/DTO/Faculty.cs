using System.ComponentModel.DataAnnotations;

namespace AcademicPerformance.Models.DTO
{
    /// <summary>
    /// Модель факультета
    /// </summary>
    public class Faculty
    {
        /// <summary>
        /// Идентификатор факультета
        /// </summary>
        [Key]
        public int FacultyId { get; set; }
        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        public string Name { get; set; }
    }
}
