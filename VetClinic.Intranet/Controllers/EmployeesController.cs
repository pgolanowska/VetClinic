using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Staff;
using System.Web;
using VetClinic.Data.Data.Clinic;

namespace VetClinic.Intranet.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly VetClinicContext _context;

        public EmployeesController(VetClinicContext context)
        {
            _context = context;
        }
        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return _context.Employee != null ?
                        View(await _context.Employee
                                .Include(e => e.EmployeeServiceGroups)
                                .ThenInclude(esg => esg.ServiceGroup)
                                .Join(_context.Title, employee => employee.TitleId, title => title.TitleId, (employee, title) => new { employee, title })
                                .Join(_context.Position, et => et.employee.PositionId, position => position.PositionId, (et, position) => new { et.employee, et.title, position })
                                .Where(e => e.employee.EmployeeIsActive == true)
                                .Select(e => new Employee
                                {
                                    EmployeeId = e.employee.EmployeeId,
                                    EmployeeName = e.employee.EmployeeName,
                                    EmployeeSurname = e.employee.EmployeeSurname,
                                    Title = e.title,
                                    Position = e.position,
                                    EmployeeEducation = e.employee.EmployeeEducation,
                                    EmployeeBio = e.employee.EmployeeBio,
                                    EmployeePhoto = e.employee.EmployeePhoto,
                                    EmployeeServiceGroups = e.employee.EmployeeServiceGroups
                                }).ToListAsync()) :
                        Problem("Entity set 'VetClinicContext.Employee'  is null.");
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.Positions = (from position in _context.Position select position).ToList();
            ViewBag.Titles = (from title in _context.Title select title).ToList();
            ViewBag.ServiceGroups = (from serviceGroup in _context.ServiceGroup select serviceGroup).ToList();
            Employee employee = new Employee();
            return View(employee);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,EmployeeSurname,TitleId,PositionId,EmployeeEducation,EmployeeBio,EmployeeIsActive")] Employee employee, [FromForm] IFormFile EmployeePhotoFile, List<int> SelectedServiceGroups)
        {
            ViewBag.Positions = (from position in _context.Position select position).ToList();
            ViewBag.Titles = (from title in _context.Title select title).ToList();
            ViewBag.ServiceGroups = (from serviceGroup in _context.ServiceGroup select serviceGroup).ToList();

            if (EmployeePhotoFile != null && EmployeePhotoFile.Length > 0)
            {
                byte[] employeePhoto = new byte[] { };
                using (var memoryStream = new MemoryStream())
                {
                    await EmployeePhotoFile.CopyToAsync(memoryStream);
                    employeePhoto = memoryStream.ToArray();
                }
                employee.EmployeePhoto = employeePhoto;
            }

            

            //if (ModelState.IsValid)
            //{
            _context.Add(employee);
            await _context.SaveChangesAsync();

            foreach (var serviceGroup in SelectedServiceGroups)
            {
                var newLink = new EmployeeServiceGroup
                {
                    EmployeeId = employee.EmployeeId,
                    ServiceGroupId = serviceGroup
                };
                _context.EmployeeServiceGroup.Add(newLink);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
            //}

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Positions = (from position in _context.Position select position).ToList();
            ViewBag.Titles = (from title in _context.Title select title).ToList();
            ViewBag.ServiceGroups = (from serviceGroup in _context.ServiceGroup select serviceGroup).ToList();
            ViewBag.SelectedGroups = (from empServiceGroup in _context.EmployeeServiceGroup where empServiceGroup.EmployeeId == id select empServiceGroup.ServiceGroupId).ToList();

            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,EmployeeSurname,TitleId,PositionId,EmployeeEducation,EmployeeBio")] Employee employee, List<int> SelectedServiceGroups)
        {
            ViewBag.Positions = (from position in _context.Position select position).ToList();
            ViewBag.Titles = (from title in _context.Title select title).ToList();
            ViewBag.ServiceGroups = (from serviceGroup in _context.ServiceGroup select serviceGroup).ToList();
            ViewBag.SelectedGroups = (from serviceGroup in _context.EmployeeServiceGroup where serviceGroup.EmployeeId == id select serviceGroup).ToList();

            var existingEmployee = await _context.Employee.FindAsync(id);

           existingEmployee.EmployeeName = employee.EmployeeName;
           existingEmployee.EmployeeSurname = employee.EmployeeSurname;
           existingEmployee.TitleId = employee.TitleId;
           existingEmployee.PositionId = employee.PositionId;
           existingEmployee.EmployeeEducation = employee.EmployeeEducation;
           existingEmployee.EmployeeBio = employee.EmployeeBio;

            if (id != existingEmployee.EmployeeId)
            {
                return NotFound();
            }

            var existingLinks = _context.EmployeeServiceGroup.Where(esg => esg.EmployeeId == existingEmployee.EmployeeId);
            _context.EmployeeServiceGroup.RemoveRange(existingLinks);
            await _context.SaveChangesAsync();

            foreach (var serviceGroup in SelectedServiceGroups)
            {
                var newLink = new EmployeeServiceGroup
                {
                    EmployeeId = existingEmployee.EmployeeId,
                    ServiceGroupId = serviceGroup
                };
                _context.EmployeeServiceGroup.Add(newLink);
                await _context.SaveChangesAsync();
            }

            _context.Update(existingEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(existingEmployee);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!EmployeeExists(existingEmployee.EmployeeId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(existingEmployee);
        }


        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'VetClinicContext.Employee'  is null.");
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }

        public IActionResult GetPhoto(int id)
        {
            var employee = _context.Employee.FirstOrDefault(e => e.EmployeeId == id);
            if (employee?.EmployeePhoto != null)
            {
                return File(employee.EmployeePhoto, "image/jpeg");
            }
            return NotFound();
        }
    }
}
