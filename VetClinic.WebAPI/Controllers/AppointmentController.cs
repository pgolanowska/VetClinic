using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data.Clients;
using VetClinic.Data.Data;
using System.Diagnostics;
using VetClinic.WebAPI.ResourceModels;
using System.ComponentModel;
using VetClinic.Data.Data.Clinic;
using VetClinic.Data.Data.Staff;

namespace VetClinic.WebAPI.Controllers
{
    public class AppointmentController : BaseController
    {
        public AppointmentController(VetClinicContext context, UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager)
            : base(context, userManager, signInManager)
        {
        }

        [HttpGet("GetSchedule/{selectedDate}")]
        public async Task<IActionResult> GetSchedule(string selectedDate)
        {
            DateTime start = DateTime.Parse(selectedDate);
            if (start == null) start = new GregorianCalendar().AddDays(DateTime.Now, -((int)DateTime.Now.DayOfWeek) + 1);
            DateTime end = start.AddDays(1);

            var schedules = context.Employee
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
                    .GroupBy(x => new { x.e.EmployeeId, x.e.EmployeeName, x.e.EmployeeSurname, x.e.EmployeePhoto })
                    .Select(g => new ScheduleResourceModel
                    {
                        EmployeeId = g.Key.EmployeeId,
                        EmployeeName = g.Key.EmployeeName + " " + g.Key.EmployeeSurname,
                        EmployeePhoto = g.Key.EmployeePhoto,
                        DaySchedule = !g.Any(s => s.es != null && s.es.IsWorking)
                                        ? new List<string> { "Off" }
                                        : g.Where(s => s.es != null && s.es.IsWorking)
                                            .SelectMany(s => GenerateHourlyIntervals(s.es.StartTime, s.es.EndTime))
                                            .ToList()
                    }).ToList();

            return Ok(schedules);
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

        [HttpPost("BookAppointment/{userId}")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentResourceModel appointment, string userId)
        {
            {
                int clientId = context.ClientUser.Where(c => c.Id == userId).Select(c => c.ClientId).FirstOrDefault();
                var newAppointment = new Appointment
                {
                    EmployeeId = appointment.EmployeeId,
                    AppointmentDateTime = appointment.AppointmentDateTime,
                    ClientId = clientId,
                    OwnerName = appointment.OwnerName,
                    OwnerPhoneNumber = appointment.OwnerPhoneNumber,
                    OwnerEmail = appointment.OwnerEmail,
                    PetId = appointment.PetId,
                    PetName = appointment.PetName,
                    IssueDescription = appointment.IssueDescription
                };
                context.Appointment.Add(newAppointment);
                await context.SaveChangesAsync();
                return Ok();
            }
        }


        [HttpGet("User/{userId}/Appointments/{param}")]
        public async Task<IActionResult> GetUserAppointments(string userId, string param)
        {
            int clientId = context.ClientUser.Where(c => c.Id == userId).Select(c => c.ClientId).FirstOrDefault();
            var appointments = context.Appointment
                .Include(ap => ap.Employee)
                .Where(ap => ap.ClientId == clientId)
                .Where(ap => ap.IsActive == true);

            var usersAppointments = new List<AppointmentResourceModel>();

            foreach (var item in appointments)
            {
                var appointment = new AppointmentResourceModel
                {
                    AppointmentId = item.AppointmentId,
                    EmployeeId = item.EmployeeId,
                    EmployeeName = item.Employee.EmployeeName,
                    EmployeeSurname = item.Employee.EmployeeSurname,
                    AppointmentDateTime = item.AppointmentDateTime,
                    OwnerName = item.OwnerName,
                    OwnerEmail = item.OwnerEmail,
                    OwnerPhoneNumber = item.OwnerPhoneNumber,
                    PetId = item.PetId ?? -1,
                    PetName = item.PetName,
                    IssueDescription = item.IssueDescription,
                    Status = item.IsConfirmed ? "Confirmed" : "Not Confirmed",
                };

                usersAppointments.Add(appointment);
            }

            return Ok(usersAppointments);
        }

        [HttpGet("CancelAppointment/{appointmentId}")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            var appointmentToCancel = await context.Appointment.FirstOrDefaultAsync(cp => cp.AppointmentId == appointmentId);
            appointmentToCancel.IsActive = false;

            context.Appointment.Update(appointmentToCancel);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("UpdateAppointment")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentResourceModel appointment)
        {
            var existingappointment = await context.Appointment.FindAsync(appointment.AppointmentId);
            if (existingappointment == null)
            {
                return NotFound();
            }

            existingappointment.IssueDescription = appointment.IssueDescription;

            context.Appointment.Update(existingappointment);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
