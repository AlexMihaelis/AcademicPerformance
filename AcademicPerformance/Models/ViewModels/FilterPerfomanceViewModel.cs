using AcademicPerformance.Models.DTO;

namespace AcademicPerformance.Models.ViewModels
{
    public class FilterPerfomanceViewModel
    {
        public IEnumerable<Discipline> Disciplines { get; set; }
        public int? SelectedDisciplineId { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public int? SelectedGroupId { get; set; }
        public IEnumerable<PerfomanceViewModel> Perfomance { get; set; }
    }
}
