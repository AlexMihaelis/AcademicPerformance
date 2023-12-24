using System.ComponentModel.DataAnnotations;

namespace AcademicPerformance.Models.DTO
{
    /// <summary>
    /// Модель студента
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        [Key]
        public int StudentId { get; set; }
        /// <summary>
        /// Номер зачетной книжки студента
        /// </summary>
        public int NumGradeBook { get; set; }
        /// <summary>
        /// Полное имя студента
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// Дата рождения студента
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// Коллекция оценок
        /// </summary>
        public HashSet<Grade> Grades { get; set; } = new HashSet<Grade>();
    }
}
