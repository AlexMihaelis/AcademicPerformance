using AcademicPerformance.EntityFraamewordCore;
using AcademicPerformance.Models;
using AcademicPerformance.Models.DTO;
using AcademicPerformance.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AcademicPerformance.Controllers
{
    public class PerfomanceController : Controller
    {
        private readonly ILogger<PerfomanceController> _logger;
        private readonly AcademicPerformanceDBContext dBContext;

        public PerfomanceController(ILogger<PerfomanceController> logger, AcademicPerformanceDBContext dBContext)
        {
            _logger = logger;
            this.dBContext = dBContext;
        }

        /// <summary>
        /// Страница успеваемости
        /// </summary>
        /// <returns>Возвращает страницу успеваемости с объектом, в котором содержатся справочники дисциплин и групп</returns>
        [HttpGet]
        public IActionResult Index()
        {
            // Выборка всех дисциплин
            var disciplines = dBContext.Disciplines.ToList();

            // Выборка всех групп
            var groups = dBContext.Groups.ToList();

            // Создание объекта, содержащего справочники и прочую информацию
            var model = new FilterPerfomanceViewModel()
            {
                Disciplines = disciplines,
                Groups = groups
            };
            return View(model);
        }

        /// <summary>
        /// Фильтр успеваемости
        /// Этот метод делает выборку успеваемости за текущую неделю
        /// </summary>
        /// <param name="filter">Параметры, которые содержат идентификатор группы и идентификатор дисциплины</param>
        /// <returns>"ВьюМодель", которая содержит справочники, выбранную дисциплину и группу, а также сведения об успеваемости</returns>
        [HttpPost("Index")]
        public IActionResult Filter(FilterPerfomanceViewModel filter)
        {
            if (filter.SelectedDisciplineId == null || filter.SelectedGroupId == null)
                return View("Index", filter);
        //Удалить этри запрос
            var query = dBContext.Grades
                .Include(x => x.Student)
                .Where(x => x.DisciplineId == filter.SelectedDisciplineId && x.Student.GroupId == filter.SelectedGroupId)
                .GroupBy(x => x.Student)
                .ToQueryString();

            // Выбираем из БД оценки вместе со студентами по выбранной дисциплине и группе студента
            filter.Perfomance = dBContext.Grades
                .Include(x => x.Student)
                .Where(x => x.DisciplineId == filter.SelectedDisciplineId && x.Student.GroupId == filter.SelectedGroupId)
                .GroupBy(x => x.Student)
                .AsEnumerable()
                .Select((x, i) => new PerfomanceViewModel
                {
                    Number = i,
                    StudentId = x.Key.StudentId,
                    StudentFullname = x.Key.FullName,
                    Grades = x.Select(y => new GradeViewModel
                    {
                        Value = y.Value,
                        Date = y.Date,
                        DayOfWeek = y.Date.DayOfWeek
                    })
                })
                .ToList();

            // Выбираем из БД студентов, привязанных к выбранной группе
            var query1 = dBContext.Students
                .Where(x => x.GroupId == filter.SelectedGroupId)
                .OrderBy(x => x.FullName)
                .ToQueryString();

            var students = dBContext.Students
                .Where(x => x.GroupId == filter.SelectedGroupId)
                .OrderBy(x => x.FullName)
                .ToList();

            // Создаем шаблон для незаполненного журнала оценок
            var template = new List<PerfomanceViewModel>();

            var startDate = DateTime.Now.Date;
            while (startDate.DayOfWeek != DayOfWeek.Monday)
                startDate = startDate.AddDays(-1);

            // Проходимся по студентов в выбранной группе, используем Select, чтобы индексировать список студентов
            int i = 0;
            foreach (var student in students)
            {
                // Заполняем шаблон
                template.Add(new PerfomanceViewModel
                {
                    StudentId = student.StudentId,
                    Number = i + 1,
                    StudentFullname = student.FullName,
                    Grades = Enumerable.Range(0, 6).Select(x => new GradeViewModel
                    {
                        Date = startDate.AddDays(x),
                        DayOfWeek = startDate.AddDays(x).DayOfWeek
                    }).ToList()
                });
                i++;
            }

            // Заполняем в шаблон оценки, если они есть
            if (filter.Perfomance.Any())
            {
                foreach (var item in template)
                {
                    var studentInfo = filter.Perfomance.FirstOrDefault(x => x.StudentId == item.StudentId);
                    if (studentInfo != null)
                    {
                        foreach (var grade in item.Grades)
                        {
                            grade.Value = studentInfo.Grades.FirstOrDefault(x => x.Date == grade.Date)?.Value!;
                        }
                    }
                }
            }

            filter.Perfomance = template;

            return View("Index", filter);
        }

        /// <summary>
        /// Сохранение ведомости
        /// </summary>
        /// <param name="grades">Коллекция, которая содержит оценки</param>
        /// <returns>Статус запроса</returns>
        [HttpPost("Save")]
        public IActionResult SavePerfomance(List<Grade> grades)
        {
            if (grades == null || !grades.Any()) return BadRequest("Не указаны оценки");

            var existsGrades = dBContext.Grades.ToList().Where(x => grades.Any(y => y.Date == x.Date && y.StudentId == x.StudentId && y.DisciplineId == x.DisciplineId));

            if (existsGrades.Any())
            {
                foreach (var grade in grades)
                {
                    var existGrade = existsGrades.FirstOrDefault(x => x.Date == grade.Date && x.StudentId == grade.StudentId && x.DisciplineId == grade.DisciplineId);
                    if (existGrade != null)
                    {
                        existGrade.Value = grade.Value;
                    }
                }
            }

            var insertGrades = grades.Where(x => !existsGrades.Any(y => y.Date == x.Date && y.StudentId == x.StudentId && y.DisciplineId == x.DisciplineId));
            dBContext.Grades.AddRange(insertGrades);
            dBContext.ChangeTracker.DetectChanges();
            var debug = dBContext.ChangeTracker.ToDebugString();
            dBContext.SaveChanges();

            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}