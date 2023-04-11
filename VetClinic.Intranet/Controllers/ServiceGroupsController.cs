using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clinic;

namespace VetClinic.Intranet.Controllers
{
    public class ServiceGroupsController : Controller
    {
        private readonly VetClinicContext _context;

        public ServiceGroupsController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: ServiceGroups
        public async Task<IActionResult> Index()
        {
              return _context.ServiceGroup != null ? 
                          View(await _context.ServiceGroup.ToListAsync()) :
                          Problem("Entity set 'VetClinicContext.ServiceGroup'  is null.");
        }

        // GET: ServiceGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServiceGroup == null)
            {
                return NotFound();
            }

            var serviceGroup = await _context.ServiceGroup
                .FirstOrDefaultAsync(m => m.ServiceGroupId == id);
            if (serviceGroup == null)
            {
                return NotFound();
            }

            return View(serviceGroup);
        }

        // GET: ServiceGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceGroupId,ServiceGroupName,ServiceGroupDesc,ServiceGroupIsActive")] ServiceGroup serviceGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceGroup);
        }

        // GET: ServiceGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServiceGroup == null)
            {
                return NotFound();
            }

            var serviceGroup = await _context.ServiceGroup.FindAsync(id);
            if (serviceGroup == null)
            {
                return NotFound();
            }
            return View(serviceGroup);
        }

        // POST: ServiceGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceGroupId,ServiceGroupName,ServiceGroupDesc,ServiceGroupIsActive")] ServiceGroup serviceGroup)
        {
            if (id != serviceGroup.ServiceGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceGroupExists(serviceGroup.ServiceGroupId))
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
            return View(serviceGroup);
        }

        // GET: ServiceGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServiceGroup == null)
            {
                return NotFound();
            }

            var serviceGroup = await _context.ServiceGroup
                .FirstOrDefaultAsync(m => m.ServiceGroupId == id);
            if (serviceGroup == null)
            {
                return NotFound();
            }

            return View(serviceGroup);
        }

        // POST: ServiceGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServiceGroup == null)
            {
                return Problem("Entity set 'VetClinicContext.ServiceGroup'  is null.");
            }
            var serviceGroup = await _context.ServiceGroup.FindAsync(id);
            if (serviceGroup != null)
            {
                _context.ServiceGroup.Remove(serviceGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceGroupExists(int id)
        {
          return (_context.ServiceGroup?.Any(e => e.ServiceGroupId == id)).GetValueOrDefault();
        }
    }
}
