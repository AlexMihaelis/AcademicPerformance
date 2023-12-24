using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademicPerformance.Models.DTO
{
    /// <summary>
    /// Модель дисциплины
    /// </summary>
    public class Discipline
    {
        /// <summary>
        /// Идентификатор дисципоины
        /// </summary>
        [Key]
        public int DisciplineId { get; set; }
        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        public string Name { get; set; }
    }
}
