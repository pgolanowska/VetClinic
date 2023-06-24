using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clients;

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
        }
    }
}
