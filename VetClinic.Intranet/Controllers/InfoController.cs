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
    public class InfoController : Controller
    {
        private readonly VetClinicContext _context;

        public InfoController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: Info
        public async Task<IActionResult> Index()
        {
              return _context.Info != null ? 
                          View(await _context.Info.ToListAsync()) :
                          Problem("Entity set 'VetClinicContext.Info'  is null.");
        }

        // GET: Info/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Info == null)
            {
                return NotFound();
            }

            var info = await _context.Info
                .FirstOrDefaultAsync(m => m.InfoId == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }

        // GET: Info/Create
        public IActionResult Create()
        {
            Info info = new Info();
            return View(info);
        }

        // POST: Info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InfoId,InfoTitle,InfoDesc,InfoIsActive")] Info info)
        {
            if (ModelState.IsValid)
            {
                _context.Add(info);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(info);
        }

        // GET: Info/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Info == null)
            {
                return NotFound();
            }

            var info = await _context.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }

        // POST: Info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InfoId,InfoTitle,InfoDesc,InfoIsActive")] Info info)
        {
            if (id != info.InfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(info);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoExists(info.InfoId))
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
            return View(info);
        }

        // GET: Info/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Info == null)
            {
                return NotFound();
            }

            var info = await _context.Info
                .FirstOrDefaultAsync(m => m.InfoId == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }

        // POST: Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Info == null)
            {
                return Problem("Entity set 'VetClinicContext.Info'  is null.");
            }
            var info = await _context.Info.FindAsync(id);
            if (info != null)
            {
                _context.Info.Remove(info);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoExists(int id)
        {
          return (_context.Info?.Any(e => e.InfoId == id)).GetValueOrDefault();
        }
    }
}
