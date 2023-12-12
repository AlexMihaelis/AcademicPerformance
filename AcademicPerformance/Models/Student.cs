namespace AcademicPerformance.Models
{
    public class Student
    {
        public int StudentsID { get; set; }
        public int NumGradeBook { get; set; }
        public string FullName { get; set; }
        public int GroupsID { get; set; }
        public DateOnly Birthday { get; set; }

    }
}
