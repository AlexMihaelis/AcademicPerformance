using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AcademicPerformance.Models.DTO
{
    /// <summary>
    /// Модель оценки
    /// </summary>
    public class Grade
    {
        /// <summary>
        /// Идентификатор оценки
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }
        /// <summary>
        /// Идентификатор студентов
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// Идентификатор дисциплины
        /// </summary>
        public int DisciplineId { get; set; }
        /// <summary>
        /// значение
        /// </summary>
        public string? Value { get; set; }
        /// <summary>
        /// Дата выставления оценки
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Навигационное свойство "Студент"
        /// </summary>
        public Student Student { get; set; }
    }
}
