using Microsoft.AspNetCore.Mvc;
using VetClinic.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Portal.Controllers
{
    public class StaffComponent : ViewComponent
    {
        private readonly VetClinicContext _context;
        public StaffComponent(VetClinicContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("StaffComponent", await _context.Employee.Where(e => e.PositionId == 1).OrderBy(e => e.EmployeeSurname).Include(s => s.EmployeeServiceGroups).ToListAsync());
        }
    }
}
