﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Intranet.Data;
using VetClinic.Intranet.Models.Staff;

namespace VetClinic.Intranet.Controllers
{
    public class TitlesController : Controller
    {
        private readonly VetClinicContext _context;

        public TitlesController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: Titles
        public async Task<IActionResult> Index()
        {
              return _context.Title != null ? 
                          View(await _context.Title.ToListAsync()) :
                          Problem("Entity set 'VetClinicContext.Title'  is null.");
        }

        // GET: Titles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Title == null)
            {
                return NotFound();
            }

            var title = await _context.Title
                .FirstOrDefaultAsync(m => m.TitleId == id);
            if (title == null)
            {
                return NotFound();
            }

            return View(title);
        }

        // GET: Titles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Titles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TitleId,TitleName,TitleIsActive")] Title title)
        {
            if (ModelState.IsValid)
            {
                _context.Add(title);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(title);
        }

        // GET: Titles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Title == null)
            {
                return NotFound();
            }

            var title = await _context.Title.FindAsync(id);
            if (title == null)
            {
                return NotFound();
            }
            return View(title);
        }

        // POST: Titles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TitleId,TitleName,TitleIsActive")] Title title)
        {
            if (id != title.TitleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(title);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TitleExists(title.TitleId))
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
            return View(title);
        }

        // GET: Titles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Title == null)
            {
                return NotFound();
            }

            var title = await _context.Title
                .FirstOrDefaultAsync(m => m.TitleId == id);
            if (title == null)
            {
                return NotFound();
            }

            return View(title);
        }

        // POST: Titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Title == null)
            {
                return Problem("Entity set 'VetClinicContext.Title'  is null.");
            }
            var title = await _context.Title.FindAsync(id);
            if (title != null)
            {
                _context.Title.Remove(title);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TitleExists(int id)
        {
          return (_context.Title?.Any(e => e.TitleId == id)).GetValueOrDefault();
        }
    }
}
