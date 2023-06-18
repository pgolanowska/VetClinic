using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;

namespace VetClinic.Intranet.Controllers.Components
{
    public class TodaysScheduleComponent : ViewComponent
    {
        private readonly VetClinicContext _context;
        public TodaysScheduleComponent(VetClinicContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("TodaysScheduleComponent", await _context.EmployeeSchedule.Include(e => e.Employee).Where(es => es.ScheduleDate.Date == DateTime.Today).OrderBy(es => es.StartTime).ToListAsync());
        }
    }
}
