using AcademicPerformance.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace AcademicPerformance.EntityFraamewordCore;
public class AcademicPerformanceDBContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<Faculty> Faculties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=AcademicPerformance;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
