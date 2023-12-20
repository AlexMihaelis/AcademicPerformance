namespace AcademicPerformance.Models.ViewModels
{
    public class PerfomanceViewModel
    {
        public int Number { get; set; }
        public string StudentFullname { get; set; }
        public IEnumerable<GradeViewModel> Grades { get; set; }
    }
}
