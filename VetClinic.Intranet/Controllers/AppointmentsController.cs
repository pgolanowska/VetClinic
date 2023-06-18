using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clinic;
using VetClinic.Intranet.Models;

namespace VetClinic.Intranet.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly VetClinicContext _context;
        private readonly EmailService _emailService;
        public AppointmentsController(VetClinicContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Appointments
        public async Task<IActionResult> Index(string? appPeriod)
        {
            var vetClinicContext = _context.Appointment.Include(a => a.Employee)
                .Where(a => a.IsActive);
            if (appPeriod == "Past")
            {
                vetClinicContext = vetClinicContext.Where(a => a.AppointmentDateTime.Date < DateTime.Today);
            }
            else if (appPeriod == "Today")
            {
                vetClinicContext = vetClinicContext.Where(a => a.AppointmentDateTime.Date == DateTime.Today);
            }
            else if (appPeriod == "Future")
            {
                vetClinicContext = vetClinicContext.Where(a => a.AppointmentDateTime.Date > DateTime.Today);
            }
            return View(await vetClinicContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            Appointment apnt = new Appointment();
            ViewBag.Employees = (from emp in _context.Employee where emp.EmployeeIsActive == true select new { EmployeeId = emp.EmployeeId, EmployeeName = emp.EmployeeName + " " + emp.EmployeeSurname }).ToList();
            return View(apnt);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,EmployeeId,AppointmentDateTime,OwnerName,OwnerPhoneNumber,OwnerEmail,PetName,IssueDescription")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Employees = (from emp in _context.Employee where emp.EmployeeIsActive == true select new { EmployeeId = emp.EmployeeId, EmployeeName = emp.EmployeeName + " " + emp.EmployeeSurname }).ToList();
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewBag.Employees = (from emp in _context.Employee where emp.EmployeeIsActive == true select new { EmployeeId = emp.EmployeeId, EmployeeName = emp.EmployeeName + " " + emp.EmployeeSurname }).ToList();
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,EmployeeId,AppointmentDateTime,OwnerName,OwnerPhoneNumber,OwnerEmail,PetName,IssueDescription,IsConfirmed")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Employees = (from emp in _context.Employee where emp.EmployeeIsActive == true select new { EmployeeId = emp.EmployeeId, EmployeeName = emp.EmployeeName + " " + emp.EmployeeSurname}).ToList();
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointment == null)
            {
                return Problem("Entity set 'VetClinicContext.Appointment'  is null.");
            }
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                appointment.IsActive = false;
                _context.Update(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> ConfirmAppointment(int id)
        {
            var appointment = _context.Appointment.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.IsConfirmed = true;
            _context.SaveChanges();

            TempData["ConfirmationMessage"] = "The appointment has been successfully confirmed.";

            var Employee = (from emp in _context.Employee
                            where emp.EmployeeId == appointment.EmployeeId
                            select emp.EmployeeName + " " + emp.EmployeeSurname).FirstOrDefault();

            var subject = "Your VetClinic appointment details";
            var message = $"Dear {appointment.OwnerName},<br/><br/>Your appointment for {appointment.PetName} with {Employee} on {appointment.AppointmentDateTime.AddHours(2)} has been confirmed.<br/><br/>Thank you very much!<br/>VetClinic";
            await _emailService.SendEmailAsync(appointment.OwnerEmail, subject, message);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ConfirmArrival(int id)
        {
            var appointment = _context.Appointment.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.HasArrived = true;
            _context.SaveChanges();

            TempData["ConfirmationMessage"] = "Arrival confirmed.";

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> MarkComplete(int id)
        {
            var appointment = _context.Appointment.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.IsCompleted = true;
            _context.SaveChanges();

            TempData["ConfirmationMessage"] = "Appointment marked as complete.";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetAvailableSlots(int employeeId, DateTime date)
        {
            var schedule = _context.EmployeeSchedule
                .Where(s => s.EmployeeId == employeeId && s.ScheduleDate.Date == date)
                .FirstOrDefault();

            if (schedule == null)
            {
                return NotFound();
            }

            var existingAppointments = _context.Appointment
                .Where(a => a.EmployeeId == employeeId && a.AppointmentDateTime.Date == date)
                .Select(a => a.AppointmentDateTime.TimeOfDay)
                .ToList();

            // Determine the available appointment slots
            var availableSlots = new List<TimeSpan>();

            for (var time = schedule.StartTime; time < schedule.EndTime; time += TimeSpan.FromHours(1))
            {
                if (existingAppointments == null || !existingAppointments.Contains((TimeSpan)time))
                {
                    availableSlots.Add((TimeSpan)time);
                }
            }

            return Ok(availableSlots);
        }
    }
}
