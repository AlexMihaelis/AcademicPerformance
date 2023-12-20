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

        [HttpGet]
        public IActionResult Index()
        {
            var disciplines = dBContext.Disciplines.ToList();
            var groups = dBContext.Groups.ToList();
            var model = new FilterPerfomanceViewModel()
            {
                Disciplines = disciplines,
                Groups = groups
            };
            return View(model);
        }

        [HttpPost("Index")]
        public IActionResult Filter(FilterPerfomanceViewModel filter)
        {
            if (filter.SelectedDisciplineId != null && filter.SelectedGroupId != null)
            {
                filter.Perfomance = dBContext.Grades
                    .Include(x => x.Student)
                    .Where(x => x.DisciplinesID == filter.SelectedDisciplineId && x.Student.GroupsID == filter.SelectedGroupId)
                    .GroupBy(x => x.Student)
                    .Select((x, i) => new PerfomanceViewModel
                    {
                        Number = i,
                        StudentFullname = x.Key.FullName,
                        Grades = x.Select(y => new GradeViewModel
                        {
                            Value = y.Grades,
                            Date = y.Date,
                            DayOfWeek = y.Date.DayOfWeek
                        })
                    })
                    .ToList();
            }

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