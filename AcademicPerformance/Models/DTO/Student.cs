using System.ComponentModel.DataAnnotations;

namespace AcademicPerformance.Models.DTO
{
    public class Student
    {
        [Key]
        public int StudentsID { get; set; }
        public int NumGradeBook { get; set; }
        public string FullName { get; set; }
        public int GroupsID { get; set; }
        public DateTime Birthday { get; set; }
        public HashSet<Grade> Grades { get; set; } = new HashSet<Grade>();
    }
}
