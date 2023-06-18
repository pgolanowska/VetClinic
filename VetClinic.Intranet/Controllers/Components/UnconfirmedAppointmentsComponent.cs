using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;

namespace VetClinic.Intranet.Controllers.Components
{
    public class UnconfirmedAppointmentsComponent : ViewComponent
    {
        private readonly VetClinicContext _context;
        public UnconfirmedAppointmentsComponent(VetClinicContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("UnconfirmedAppointmentsComponent", await _context.Appointment.Include(e => e.Employee).Where(a => a.IsActive == true).Where(a => a.IsConfirmed == false).OrderBy(s => s.AppointmentDateTime).ToListAsync());
        }
    }
}
