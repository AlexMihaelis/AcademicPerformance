namespace AcademicPerformance.Models.ViewModels
{
    /// <summary>
    /// Модель представления для оценок
    /// </summary>
    public class GradeViewModel
    {
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// День недели
        /// </summary>
        public DayOfWeek DayOfWeek { get; set; }
    }
}
