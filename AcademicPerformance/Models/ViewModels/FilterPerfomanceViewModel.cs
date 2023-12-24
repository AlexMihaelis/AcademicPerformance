using AcademicPerformance.Models.DTO;

namespace AcademicPerformance.Models.ViewModels
{
    /// <summary>
    /// Модель представления, содержащая информацию о фильтрах по дисциплине и группе,
    /// а также содержащая ведомость по группе
    /// </summary>
    public class FilterPerfomanceViewModel
    {
        /// <summary>
        /// Коллекция дисциплин
        /// </summary>
        public IEnumerable<Discipline> Disciplines { get; set; }
        /// <summary>
        /// Выбранный идентификатор дисциплины
        /// </summary>
        public int? SelectedDisciplineId { get; set; }
        /// <summary>
        /// Коллекция групп
        /// </summary>
        public IEnumerable<Group> Groups { get; set; }
        /// <summary>
        /// Идентификатор выбранной группы
        /// </summary>
        public int? SelectedGroupId { get; set; }
        /// <summary>
        /// Коллекция ведомостей в разрезе студента
        /// </summary>
        public IEnumerable<PerfomanceViewModel> Perfomance { get; set; }
    }
}
