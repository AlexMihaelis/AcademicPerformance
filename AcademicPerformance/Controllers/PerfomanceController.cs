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

            var query = dBContext.Grades
                .Include(x => x.Student)
                .Where(x => x.DisciplineId == filter.SelectedDisciplineId && x.Student.GroupId == filter.SelectedGroupId)
                .GroupBy(x => x.Student);

            // Выбираем из БД оценки вместе со студентами по выбранной дисциплине и группе студента
            filter.Perfomance = dBContext.Grades
                .Include(x => x.Student)
                .Where(x => x.DisciplineId == filter.SelectedDisciplineId && x.Student.GroupId == filter.SelectedGroupId)
                .GroupBy(x => x.Student)
                .AsEnumerable()
                .Select((x, i) => new PerfomanceViewModel
                {
                    Number = i,
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
                    Number = i + 1,
                    StudentFullname = student.FullName,
                    Grades = Enumerable.Range(0, 6).Select(x => new GradeViewModel
                    {
                        Date = startDate.AddDays(x),
                        DayOfWeek = startDate.AddDays(x).DayOfWeek
                    })
                });
                i++;
            }

            // Заполняем в шаблон оценки, если они есть
            if (filter.Perfomance.Any())
            {
                foreach (var item in template)
                {
                    var studentInfo = filter.Perfomance.FirstOrDefault(x => x.StudentFullname == item.StudentFullname);
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