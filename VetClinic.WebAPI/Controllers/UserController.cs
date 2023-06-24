using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using VetClinic.Data.Data;
using VetClinic.Data.Data.Clients;
using VetClinic.WebAPI.ResourceModels;

namespace VetClinic.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {

        public UserController(VetClinicContext context, UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager)
            : base(context, userManager, signInManager)
        {
        }

        // GET: api/Users/5
        [HttpGet("GetUser/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID {userId}.");
            }
            var profile = new
            {
                Email = user.Email,
                Name = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientName).FirstOrDefault(),
                Surname = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientSurname).FirstOrDefault(),
                Address = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientAddress).FirstOrDefault(),
                PhoneNumber = context.Client.Where(c => c.ClientId == user.ClientId).Select(c => c.ClientPhoneNumber).FirstOrDefault(),
            };
            return Ok(profile);
        }

        [HttpPut("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserResourceModel userResourceModel, string userId)
        {
 
            var user = await userManager.FindByIdAsync(userId);
            user.Email = userResourceModel.Email;
            user.UserName = userResourceModel.Email;

            var existingClient = await context.Client.Where(c => c.ClientId == user.ClientId).FirstOrDefaultAsync();
            existingClient.ClientEmail = userResourceModel.Email;
            existingClient.ClientPhoneNumber = userResourceModel.PhoneNumber;
            existingClient.ClientName = userResourceModel.Name;
            existingClient.ClientSurname = userResourceModel.Surname;
            existingClient.ClientAddress = userResourceModel.Address;

            await userManager.UpdateAsync(user);
            context.Client.Update(existingClient);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("ChangePassword/{userId}")]
        public async Task<IActionResult> ChangePassword(string userId, [FromBody] ChangePasswordResourceModel changePasswordModel)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

    }
}
