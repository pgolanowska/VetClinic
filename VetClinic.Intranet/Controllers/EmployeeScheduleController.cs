using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Staff;

namespace VetClinic.Intranet.Controllers
{
    public class EmployeeScheduleController : Controller
    {
        private readonly VetClinicContext _context;

        public EmployeeScheduleController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: EmployeeSchedule
        public async Task<IActionResult> Index()
        {
            ViewBag.ClinicSchedule = (from clinicSchedule in _context.ClinicSchedule select clinicSchedule).ToList();
            ViewBag.Employees = (from employee in _context.Employee select employee).ToList();
            var schedule = _context.EmployeeSchedule.Include(e => e.Employee);
            return View(await schedule.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedulesForWeek(DateTime start, DateTime end)
        {
            //if (start == null) start = new GregorianCalendar().AddDays(DateTime.Now, -((int)DateTime.Now.DayOfWeek) + 1);
            //if (end == null) end = start.AddDays(4);
            var schedules = _context.Employee
                            .Where(e => e.EmployeeIsActive == true)
                            .GroupJoin(
                            _context.EmployeeSchedule,
                        e => e.EmployeeId,
                        es => es.EmployeeId,
                        (e, es) => new { e, es })
                    .SelectMany(
                        x => x.es.DefaultIfEmpty(),
                        (x, es) => new { e = x.e, es = (es == null || (es.ScheduleDate >= start && es.ScheduleDate <= end)) ? es : null })
                    .OrderBy(x => x.e.EmployeeSurname)
                    .ToList()
                    .GroupBy(x => new { x.e.EmployeeId, x.e.EmployeeName, x.e.EmployeeSurname })
                    .Select(g => new
                    {
                        employeeId = g.Key.EmployeeId,
                        employeeName = g.Key.EmployeeName + " " + g.Key.EmployeeSurname,
                        WeekSchedule = g.GroupBy(x => x.es == null ? -1 : ((int)x.es.ScheduleDate.DayOfWeek + 6) % 7)
                            .Where(x => x.Key != -1)
                            .ToDictionary(
                                x => x.Key,
                                x => x.Select(s => s.es == null
                                    ? new List<string> { "Off" }
                                    : new List<string> { s.es.StartTime.ToString(), s.es.EndTime.ToString() }
                                      ))
                    })
                    .ToList();

            return Json(schedules);
        }

        // GET: EmployeeSchedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeSchedule == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeScheduleId == id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }

            return View(employeeSchedule);
        }

        // GET: EmployeeSchedule/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeBio");
            return View();
        }

        // POST: EmployeeSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeScheduleId,EmployeeId,ScheduleDate,IsWorking,StartTime,EndTime,Comment")] EmployeeSchedule employeeSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeBio", employeeSchedule.EmployeeId);
            return View(employeeSchedule);
        }

        // GET: EmployeeSchedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeSchedule == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule.FindAsync(id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeBio", employeeSchedule.EmployeeId);
            return View(employeeSchedule);
        }

        // POST: EmployeeSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeScheduleId,EmployeeId,ScheduleDate,IsWorking,StartTime,EndTime,Comment")] EmployeeSchedule employeeSchedule)
        {
            if (id != employeeSchedule.EmployeeScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeScheduleExists(employeeSchedule.EmployeeScheduleId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeBio", employeeSchedule.EmployeeId);
            return View(employeeSchedule);
        }

        // GET: EmployeeSchedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeSchedule == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeScheduleId == id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }

            return View(employeeSchedule);
        }

        // POST: EmployeeSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeSchedule == null)
            {
                return Problem("Entity set 'VetClinicContext.EmployeeSchedule'  is null.");
            }
            var employeeSchedule = await _context.EmployeeSchedule.FindAsync(id);
            if (employeeSchedule != null)
            {
                _context.EmployeeSchedule.Remove(employeeSchedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeScheduleExists(int id)
        {
          return (_context.EmployeeSchedule?.Any(e => e.EmployeeScheduleId == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSchedulesForWeek(DateTime start, DateTime end)
        {
            var schedules = _context.EmployeeSchedule
                                    .Where(es => es.ScheduleDate >= start && es.ScheduleDate <= end);

            _context.EmployeeSchedule.RemoveRange(schedules);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddSchedules([FromBody] List<EmployeeSchedule> schedules)
        {
            await _context.EmployeeSchedule.AddRangeAsync(schedules);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
