using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Data.Data.Clients;
using VetClinic.Data.Data;
using VetClinic.Portal.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Portal.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager, IWebHostEnvironment hostingEnvironment, VetClinicContext context)
            :base(userManager, signInManager, hostingEnvironment, context)
        {
        }

        // GET: /User/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create new Client
                var client = new Client
                {
                    ClientName = model.FirstName,
                    ClientSurname = model.LastName,
                    ClientEmail = model.Email,
                };

                // Save the client to the database
                context.Client.Add(client);
                await context.SaveChangesAsync();

                // Create new ApplicationUser
                var user = new ClientUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    ClientId = client.ClientId
                };

                // Create the user
                var result = await userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    // If the user is successfully created, sign in the user
                    await signInManager.SignInAsync(user, isPersistent: false);

                    // Redirect to the home page
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl = Url.Action("Profile", "Account");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            returnUrl = Url.Action("Profile", "Account");
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            ProfileViewModel CurrentUser = new ProfileViewModel();
            HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(CurrentUser));
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (User == null || User.Identity == null || string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Login", "Account");
            }
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

            ViewBag.Pets = context.ClientPet.Include(cp => cp.Pet).Where(cp => cp.ClientId == user.ClientId).Where(cp => cp.Pet.PetIsActive).ToList();
            return View(CurrentUser);
        }

        [HttpGet]
        public IActionResult PetDetails(int id)
        {
            var pet = context.Pet.Include(p => p.PetSpecies).Where(p => p.PetId == id).FirstOrDefault();
            if (pet == null)
            {
                return NotFound();
            }

            return PartialView("Partial/PetDetails", pet);
        }

        [HttpGet]
        public IActionResult UserDetails()
        {
            var userJson = HttpContext.Session.GetString("CurrentUser");
            var user = JsonConvert.DeserializeObject<ProfileViewModel>(userJson);

            if (user == null)
            {
                return NotFound();
            }

            return PartialView("Partial/UserDetails", user);
        }

        public async Task<IActionResult> GetUserAppointments()
        {
            var userJson = HttpContext.Session.GetString("CurrentUser");
            var user = JsonConvert.DeserializeObject<ProfileViewModel>(userJson);

            int clientId = context.ClientUser.Where(c => c.Id == user.Id).Select(c => c.ClientId).FirstOrDefault();
            var appointments = context.Appointment
                .Include(ap => ap.Employee)
                .Include(ap => ap.Pet)
                .Where(ap => ap.ClientId == clientId)
                .Where(ap => ap.IsActive == true);

            return PartialView("Partial/AppointmentList", appointments);
        }
    }
}
