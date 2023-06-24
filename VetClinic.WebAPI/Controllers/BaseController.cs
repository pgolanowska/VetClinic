using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Data.Data.Clients;
using VetClinic.Data.Data;

namespace VetClinic.WebAPI.Controllers
{
    public class BaseController : Controller
    {
        protected readonly VetClinicContext context;
        protected readonly UserManager<ClientUser> userManager;
        protected readonly SignInManager<ClientUser> signInManager;
        public BaseController(VetClinicContext context, UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
    }
}
