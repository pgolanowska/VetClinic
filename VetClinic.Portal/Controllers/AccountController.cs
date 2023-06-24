using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Data.Data.Clients;
using VetClinic.Data.Data;
using VetClinic.Portal.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

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
                Trace.WriteLine(result);


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

            ProfileViewModel CurrentUser = JsonConvert.DeserializeObject<ProfileViewModel>(HttpContext.Session.GetString("CurrentUser"));
            return View(CurrentUser);
        }
    }
}
