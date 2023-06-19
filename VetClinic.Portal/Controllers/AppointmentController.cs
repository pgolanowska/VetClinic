using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clinic;

namespace VetClinic.Portal.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly VetClinicContext _context;
        public static int selectedServiceGroup;
        public AppointmentController(VetClinicContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            selectedServiceGroup = id ?? -1;
            ViewBag.ClinicSchedule = (from clinicSchedule in _context.ClinicSchedule select clinicSchedule).ToList();
            ViewBag.Employees = (from employee in _context.Employee select employee).ToList();
            var schedule = _context.EmployeeSchedule.Include(e => e.Employee);
            return View(await schedule.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedulesForWeek(DateTime start, DateTime end)
        {
            if(start == null) start = new GregorianCalendar().AddDays(DateTime.Now, -((int)DateTime.Now.DayOfWeek) + 1);
            if(end == null) end = start.AddDays(4);

            var schedules = _context.Employee
                            .Where(e => selectedServiceGroup == -1 || e.EmployeeServiceGroups.Any(esg => esg.ServiceGroupId == selectedServiceGroup))
                            .GroupJoin(
                            _context.EmployeeSchedule,
                        e => e.EmployeeId,
                        es => es.EmployeeId,
                        (e, es) => new { e, es })
                    .SelectMany(
                        x => x.es.DefaultIfEmpty(),
                        (x, es) => new { e = x.e, es = (es == null || (es.ScheduleDate >= start && es.ScheduleDate <= end)) ? es : null })
                    .Where(pos => pos.e.PositionId == 1)
                    .OrderBy(x => x.e.EmployeeSurname)
                    .ToList()
                    .GroupBy(x => new { x.e.EmployeeId, x.e.EmployeeName, x.e.EmployeeSurname })
                    .Select(g => new
                    {
                        employeeId = g.Key.EmployeeId,
                        employeeName = g.Key.EmployeeName + " " + g.Key.EmployeeSurname,
                        WeekSchedule = g.GroupBy(x => x.es == null ? -1 : ((int)x.es.ScheduleDate.DayOfWeek + 6) % 7)
                            .Where(x => x.Key != -1) // Exclude unscheduled days from the final output
                            .ToDictionary(
                                x => x.Key,
                                x => x.Select(s => s.es == null
                                    ? new List<string> { "Off" }
                                    : s.es.IsWorking
                                        ? GenerateHourlyIntervals(s.es.StartTime, s.es.EndTime)
                                        : new List<string> { "Off" }))

                    })
                    .ToList();

            return Json(schedules);
        }

        public List<string> GenerateHourlyIntervals(TimeSpan? startTime, TimeSpan? endTime)
        {
            var intervals = new List<string>();

            for (var hour = startTime.Value.Hours; hour < endTime.Value.Hours; hour++)
            {
                intervals.Add($"{hour:00}:00"); // - {(hour + 1):00}:00
            }

            return intervals;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment newAppointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newAppointment);
                await _context.SaveChangesAsync();

                return Ok(newAppointment);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IActionResult GetBookedAppointmentsForWeek(DateTime start, DateTime end)
        {
            var appointments = _context.Appointment
                .Where(a => a.AppointmentDateTime >= start && a.AppointmentDateTime <= end)
                .Where(a => a.IsActive == true)
                .Select(a => new
                {
                    EmployeeId = a.EmployeeId,
                    AppointmentDateTime = a.AppointmentDateTime
                })
                .ToList();
            return Json(appointments);
        }
    }
}
