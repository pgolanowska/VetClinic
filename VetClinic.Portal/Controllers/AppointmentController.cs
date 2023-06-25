using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clients;
using VetClinic.Data.Data.Clinic;
using VetClinic.Portal.ViewModels;

namespace VetClinic.Portal.Controllers
{
    public class AppointmentController : BaseController
    {
        public static int selectedServiceGroup;
        ProfileViewModel Profile { get; set; }
        public AppointmentController(UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager, IWebHostEnvironment hostingEnvironment, VetClinicContext context)
            : base(userManager, signInManager, hostingEnvironment, context)
        {
        }

        public async Task<IActionResult> Index(int? id)
        {
            selectedServiceGroup = id ?? -1;
            ViewBag.ClinicSchedule = (from clinicSchedule in context.ClinicSchedule select clinicSchedule).ToList();
            ViewBag.Employees = (from employee in context.Employee select employee).ToList();
            var schedule = context.EmployeeSchedule.Include(e => e.Employee);

            if (HttpContext?.Session?.GetString("CurrentUser") == null)
            {
                Profile = new ProfileViewModel();
            }
            else
            {
                Profile = JsonConvert.DeserializeObject<ProfileViewModel>(HttpContext.Session.GetString("CurrentUser"));
                var user = this.userManager?.GetUserAsync(User).Result;
                //var userPets = context.ClientPet.Include(cp => cp.Pet)
                //    .Where(cp => cp.ClientId == user.ClientId)
                //    .Where(cp => cp.Pet.PetIsActive)
                //    .ToList();

                //ViewBag.UserPets = userPets.Select(p => new SelectListItem
                //{
                //    Value = p.PetId.ToString(),
                //    Text = p.Pet.PetName
                //}).ToList();

            }

            ViewBag.CurrentUser = Profile;
            return View(await schedule.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedulesForWeek(DateTime start, DateTime end)
        {
            if(start == null) start = new GregorianCalendar().AddDays(DateTime.Now, -((int)DateTime.Now.DayOfWeek) + 1);
            if(end == null) end = start.AddDays(4);

            var schedules = context.Employee
                            .Where(e => selectedServiceGroup == -1 || e.EmployeeServiceGroups.Any(esg => esg.ServiceGroupId == selectedServiceGroup))
                            .GroupJoin(
                            context.EmployeeSchedule,
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
            var userJson = HttpContext.Session.GetString("CurrentUser");
            var user = JsonConvert.DeserializeObject<ProfileViewModel>(userJson);
            int clientId = context.ClientUser.Where(c => c.Id == user.Id).Select(c => c.ClientId).FirstOrDefault();

            newAppointment.ClientId = clientId;
            if (ModelState.IsValid)
            {
                context.Add(newAppointment);
                await context.SaveChangesAsync();

                return Ok(newAppointment);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IActionResult GetBookedAppointmentsForWeek(DateTime start, DateTime end)
        {
            var appointments = context.Appointment
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
