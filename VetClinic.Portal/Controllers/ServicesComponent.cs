using Microsoft.AspNetCore.Mvc;
using VetClinic.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Portal.Controllers
{
    public class ServicesComponent : ViewComponent
    {
        private readonly VetClinicContext _context;
        public ServicesComponent(VetClinicContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("ServicesComponent", await _context.ServiceGroup.OrderBy(s => s.ServiceGroupName).ToListAsync());
        }
    }
}
