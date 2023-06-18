using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;

namespace VetClinic.Intranet.Controllers
{
    public abstract class BaseController<TEntity> : Controller where TEntity : class
    {
        protected readonly VetClinicContext _context;

        public BaseController(VetClinicContext context)
        {
            _context = context;
        }
        public abstract Task<List<TEntity>> GetEntityList();
        public abstract TEntity GetNewEntity();
        public abstract Task<TEntity> GetEntityById(int? id);
        public async Task<IActionResult> Index()
        {
            return View(await GetEntityList());
        }
        public virtual Task SetSelectList()
        {
            return null;
        }
        public async Task<IActionResult> Create()
        {
            await SetSelectList();
            return View(GetNewEntity());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var entity = await GetEntityById(id);

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            await SetSelectList();

            if (id == null)
            {
                return NotFound();
            }

            var entity = await GetEntityById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }
    }
}