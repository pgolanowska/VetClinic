using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clinic;
using VetClinic.Data.Data.CMS;
using VetClinic.Portal.Models.BusinessLogic;
using VetClinic.Portal.ViewModels;

namespace VetClinic.Portal.Controllers
{
    public class SavedItemsController : Controller
    {
        public readonly VetClinicContext context;
        public SavedItemsController(VetClinicContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            SavedItems items = new SavedItems(context, this.HttpContext);
            List<SavedItem> itemData = await items.GetSavedItems();
            return View(itemData);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAppointmentForLater([FromBody] SavedAppointmentViewModel savedAppointment)
        {
            SavedItems items = new SavedItems(context, this.HttpContext);
            items.SaveItem(savedAppointment);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SaveDoctor(int id)
        {
            SavedItems items = new SavedItems(context, this.HttpContext);
            items.SaveItem(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var savedItem = await context.SavedItem.FindAsync(id);

            if (savedItem == null)
            {
                return NotFound();
            }

            context.SavedItem.Remove(savedItem);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
