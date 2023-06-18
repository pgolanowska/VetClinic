using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clinic;
using VetClinic.Data.Data.Staff;

namespace VetClinic.Intranet.Controllers
{
    public class ServicesController : BaseController<Service>
    {
        public ServicesController(VetClinicContext context)
            :base(context)
        {
        }

        public override async Task<List<Service>> GetEntityList()
        {
            return await (from service in _context.Service
                           join serviceGroup in _context.ServiceGroup on service.ServiceGroupId equals serviceGroup.ServiceGroupId
                           where service.IsActive == true
                           select new Service
                           {
                               ServiceId = service.ServiceId,
                               ServiceName = service.ServiceName,
                               ServiceDesc = service.ServiceDesc,
                               ServiceGroup = serviceGroup
                           }).ToListAsync();
        }

        public override async Task SetSelectList()
        {
            var serviceGroups = await (from sg in _context.ServiceGroup select sg).ToListAsync();
            ViewBag.ServiceGroups = new SelectList(serviceGroups, "ServiceGroupId", "ServiceGroupName");
        }
        public override Service GetNewEntity()
        {
            return new Service();
        }
        public override async Task<Service> GetEntityById(int? id)
        {
            return await _context.Service.FirstOrDefaultAsync(m => m.ServiceId == id);
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Service == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceName,ServiceDesc,ServiceGroupId")] Service service)
        {
            ViewBag.ServiceGroups = (from sg in _context.ServiceGroup select sg).ToList();

            if (id != service.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceId))
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
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Service == null)
            {
                return Problem("Entity set 'VetClinicContext.Service'  is null.");
            }
            var service = await _context.Service.FindAsync(id);
            if (service != null)
            {
                _context.Service.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
          return (_context.Service?.Any(e => e.ServiceId == id)).GetValueOrDefault();
        }
    }
}
