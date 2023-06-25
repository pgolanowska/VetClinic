using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clients;
using VetClinic.Portal.ViewModels;

namespace VetClinic.Portal.Controllers
{
    public class BaseController : Controller
    {
        protected readonly VetClinicContext context;
        protected readonly IWebHostEnvironment hostingEnvironment;
        protected readonly UserManager<ClientUser> userManager;
        protected readonly SignInManager<ClientUser> signInManager;

        public BaseController(UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager, IWebHostEnvironment hostingEnvironment, VetClinicContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostingEnvironment = hostingEnvironment;
            this.context = context;
            if (User != null)
            {
                var user = this.userManager?.GetUserAsync(User).Result;
           
                ProfileViewModel CurrentUser = new ProfileViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientName).FirstOrDefault(),
                    Surname = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientSurname).FirstOrDefault(),
                    PhoneNumber = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientPhoneNumber).SingleOrDefault(),
                    Address = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientAddress).SingleOrDefault(),
                };

                HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(CurrentUser));
            }
        }
    }
}
