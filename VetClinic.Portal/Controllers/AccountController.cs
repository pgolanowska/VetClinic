﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Data.Data.Clients;
using VetClinic.Data.Data;
using VetClinic.Portal.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace VetClinic.Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ClientUser> userManager;
        private readonly SignInManager<ClientUser> signInManager;
        private readonly VetClinicContext context;

        public AccountController(UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager, VetClinicContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
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
                Trace.WriteLine("hrrrr");
                return RedirectToAction("Login", "Account");
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var model = new ProfileViewModel
            {
                Email = user.Email,
                Name = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientName).FirstOrDefault(),
                Surname = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientSurname).FirstOrDefault(),
            };

            return View(model);
        }
    }
}
