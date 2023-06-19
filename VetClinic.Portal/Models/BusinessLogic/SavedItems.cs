using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data;
using VetClinic.Data.Data.CMS;
using VetClinic.Portal.ViewModels;

namespace VetClinic.Portal.Models.BusinessLogic
{
    public class SavedItems
    {
        private readonly VetClinicContext context;
        private string SessionId;
        public SavedItems(VetClinicContext context, HttpContext httpContext)
        {
            this.context = context;
            SessionId = GetSessionId(httpContext);
        }
        private string GetSessionId(HttpContext httpContext)
        {
            if (httpContext.Session.GetString("SessionId") == null)
            {
                if (!string.IsNullOrWhiteSpace(httpContext.User.Identity.Name))
                {
                    httpContext.Session.SetString("SessionId", httpContext.User.Identity.Name);
                }
                else
                {
                    Guid tempSessionId = Guid.NewGuid();
                    httpContext.Session.SetString("SessionId", tempSessionId.ToString());
                }
            }
            return httpContext.Session.GetString("SessionId").ToString();
        }

        public void SaveItem(SavedAppointmentViewModel savedAppointment)
        {
            var itemToSave = (from e in context.SavedItem
                                  where e.EmployeeId == savedAppointment.EmployeeId
                                  && e.AppointmentDateTime == savedAppointment.AppointmentDateTime
                                  && e.SessionId == this.SessionId
                              select e).FirstOrDefault();

            if (itemToSave != null)
            {
            }
            else
            {
                itemToSave = new SavedItem()
                {
                    SessionId = this.SessionId,
                    IsAppointment = true,
                    EmployeeId = savedAppointment.EmployeeId,
                    AppointmentDateTime = savedAppointment.AppointmentDateTime,
                    CreatedDate = DateTime.Now
                };
                context.SavedItem.Add(itemToSave);
            }
            context.SaveChanges();
        }
        public async Task<List<SavedItem>> GetSavedItems()
        {
            return await context.SavedItem.Where(e => e.SessionId == this.SessionId).Include(e => e.Employee).ToListAsync();
        }
    }
}
