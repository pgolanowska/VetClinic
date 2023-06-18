using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;
using VetClinic.Data.Data.CMS;

namespace VetClinic.Intranet.Controllers
{
    public class InfoPagesController : Controller
    {
        private readonly VetClinicContext _context;

        public InfoPagesController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: InfoPages
        public async Task<IActionResult> Index()
        {
              return _context.InfoPage != null ? 
                          View(await _context.InfoPage.ToListAsync()) :
                          Problem("Entity set 'VetClinicContext.InfoPage'  is null.");
        }

        // GET: InfoPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InfoPage == null)
            {
                return NotFound();
            }

            var infoPage = await _context.InfoPage
                .FirstOrDefaultAsync(m => m.InfoPageId == id);
            if (infoPage == null)
            {
                return NotFound();
            }

            return View(infoPage);
        }

        // GET: InfoPages/Create
        public IActionResult Create()
        {
            InfoPage infoPage = new InfoPage();
            return View(infoPage);
        }

        // POST: InfoPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InfoPageId,InfoLinkTitle,InfoPageTitle,InfoContent,InfoLastEdited,IsActive")] InfoPage infoPage)
        {
            infoPage.InfoLastEdited = DateTime.Now;
            if (ModelState.IsValid)
            { 
                _context.Add(infoPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(infoPage);
        }

        // GET: InfoPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InfoPage == null)
            {
                return NotFound();
            }

            var infoPage = await _context.InfoPage.FindAsync(id);
            if (infoPage == null)
            {
                return NotFound();
            }
            return View(infoPage);
        }

        // POST: InfoPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InfoPageId,InfoLinkTitle,InfoPageTitle,InfoContent,InfoLastEdited,IsActive")] InfoPage infoPage)
        {
            if (id != infoPage.InfoPageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    infoPage.InfoLastEdited = DateTime.Now;
                    _context.Update(infoPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoPageExists(infoPage.InfoPageId))
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
            return View(infoPage);
        }

        // GET: InfoPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InfoPage == null)
            {
                return NotFound();
            }

            var infoPage = await _context.InfoPage
                .FirstOrDefaultAsync(m => m.InfoPageId == id);
            if (infoPage == null)
            {
                return NotFound();
            }

            return View(infoPage);
        }

        // POST: InfoPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InfoPage == null)
            {
                return Problem("Entity set 'VetClinicContext.InfoPage'  is null.");
            }
            var infoPage = await _context.InfoPage.FindAsync(id);
            if (infoPage != null)
            {
                _context.InfoPage.Remove(infoPage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoPageExists(int id)
        {
          return (_context.InfoPage?.Any(e => e.InfoPageId == id)).GetValueOrDefault();
        }
    }
}
