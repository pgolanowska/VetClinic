using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;

namespace VetClinic.Intranet.Controllers.Components
{
    public class TodaysAppointmentComponent : ViewComponent
    {
        private readonly VetClinicContext _context;
        public TodaysAppointmentComponent(VetClinicContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("TodaysAppointmentComponent", await _context.Appointment.Include(e => e.Employee).Where(a => a.IsActive == true).Where(a => a.AppointmentDateTime.Date == DateTime.Today).OrderBy(s => s.AppointmentDateTime).ToListAsync());
        }
    }
}
