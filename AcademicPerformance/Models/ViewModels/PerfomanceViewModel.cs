namespace AcademicPerformance.Models.ViewModels
{
    /// <summary>
    /// Модель представления студента с его оценками
    /// </summary>
    public class PerfomanceViewModel
    {
        /// <summary>
        /// Идентифиактор студента
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// Номер студента
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Полное имя студента
        /// </summary>
        public string StudentFullname { get; set; }
        /// <summary>
        /// Коллекция оценок
        /// </summary>
        public IEnumerable<GradeViewModel> Grades { get; set; }
    }
}
