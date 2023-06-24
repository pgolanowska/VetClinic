using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clients;
using VetClinic.WebAPI.ResourceModels;

namespace VetClinic.WebAPI.Controllers
{
    public class LoginController : BaseController
    {

        public LoginController(VetClinicContext context, UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager)
            : base(context, userManager, signInManager)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginResourceModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                return Ok(new { userId = user.Id });
            }
            return Unauthorized();
        }

        // POST: /User/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterResourceModel model)
        {
            if (ModelState.IsValid)
            {
                var client = new Client
                {
                    ClientName = model.Name,
                    ClientSurname = model.Surname,
                    ClientEmail = model.Email,
                };

                context.Client.Add(client);
                await context.SaveChangesAsync();

                var user = new ClientUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    ClientId = client.ClientId
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new { userId = user.Id });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return BadRequest(ModelState);
        }

    }
}
