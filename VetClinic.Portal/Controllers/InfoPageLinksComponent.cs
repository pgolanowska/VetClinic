using Microsoft.AspNetCore.Mvc;
using VetClinic.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Portal.Controllers
{
    public class InfoPageLinksComponent : ViewComponent
    {
        private readonly VetClinicContext _context;
        public InfoPageLinksComponent(VetClinicContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("InfoPageLinksComponent", await _context.InfoPage.ToListAsync());
        }
    }
}
