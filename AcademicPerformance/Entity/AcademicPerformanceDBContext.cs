using AcademicPerformance.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace AcademicPerformance.EntityFraamewordCore;

/// <summary>
/// Класс для регистрации контекста базы данных
/// </summary>
public class AcademicPerformanceDBContext : DbContext
{
    /// <summary>
    /// Коллекция студентов для работы с базой данных
    /// </summary>
    public DbSet<Student> Students { get; set; }

    /// <summary>
    /// Коллекция групп для работы с базой данных
    /// </summary>
    public DbSet<Group> Groups { get; set; }

    /// <summary>
    /// Коллекция оценок для работы с базой данных
    /// </summary>
    public DbSet<Grade> Grades { get; set; }

    /// <summary>
    /// Коллекция дисциплин для работы с базой данных
    /// </summary>
    public DbSet<Discipline> Disciplines { get; set; }

    /// <summary>
    /// Коллекция факультетов для работы с базой данных
    /// </summary>
    public DbSet<Faculty> Faculties { get; set; }


    /// <summary>
    /// Метод для конфигурирования базы данных
    /// </summary>
    /// <param name="optionsBuilder">Опции, с помощью которых можно конфигурировать подключение к базе данных</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=AcademicPerformance;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
