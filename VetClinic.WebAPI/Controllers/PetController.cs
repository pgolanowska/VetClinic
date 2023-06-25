using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data.Data.Clients;
using VetClinic.Data.Data;
using VetClinic.WebAPI.ResourceModels;
using System.Diagnostics;

namespace VetClinic.WebAPI.Controllers
{
    public class PetController : BaseController
    {
        public PetController(VetClinicContext context, UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager)
            : base(context, userManager, signInManager)
        {
        }

        [HttpGet("User/{userId}/Pets")]
        public async Task<IActionResult> GetUserPets(string userId)
        {
            int clientId = context.ClientUser.Where(c => c.Id == userId).Select(c => c.ClientId).FirstOrDefault();
            var clientPets = context.ClientPet
                .Include(cp => cp.Pet)
                .Include(cp => cp.Pet.PetSpecies)
                .Where(cp => cp.ClientId == clientId);

            var pets = new List<PetResourceModel>();

            foreach (var clientPet in clientPets)
            {
                var pet = new PetResourceModel
                {
                    PetId = clientPet.PetId,
                    PetName = clientPet.Pet.PetName,
                    PetSpeciesId = clientPet.Pet.PetSpeciesId,
                    PetSpecies = clientPet.Pet.PetSpecies.PetSpeciesName,
                    PetBreed = clientPet.Pet.PetBreed,
                    PetSex = clientPet.Pet.PetSex,
                    PetDateOfBirth = clientPet.Pet.PetDateOfBirth,
                    PetDistinguishingFeatures = clientPet.Pet.PetDistinguishingFeatures,
                    PetPicture = clientPet.Pet.PetPicture,
                };

                pets.Add(pet);
            }

            return Ok(pets);
        }

        [HttpPost("User/{userId}/Pets/Add")]
        public async Task<IActionResult> AddPet([FromBody] PetResourceModel pet, string userId)
        {
            int clientId = context.ClientUser.Where(c => c.Id == userId).Select(c => c.ClientId).FirstOrDefault();
            var newPet = new Pet
            {
                PetName = pet.PetName,
                PetSpeciesId = pet.PetSpeciesId,
                PetBreed = pet.PetBreed,
                PetSex = pet.PetSex,
                PetDateOfBirth = pet.PetDateOfBirth,
                PetDistinguishingFeatures = pet.PetDistinguishingFeatures,
                PetPicture = pet.PetPicture,
            };
            context.Pet.Add(newPet);
            await context.SaveChangesAsync();

            var newClientPet = new ClientPet
            {
                ClientId = clientId,
                PetId = newPet.PetId,
            };

            context.ClientPet.Add(newClientPet);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("User/{userId}/Pets/Update")]
        public async Task<IActionResult> UpdatePet([FromBody] PetResourceModel pet, string userId)
        {
            var existingPet = await context.Pet.FindAsync(pet.PetId);
            if (existingPet == null)
            {
                return NotFound();
            }

            existingPet.PetName = pet.PetName;
            existingPet.PetSpeciesId = pet.PetSpeciesId;
            existingPet.PetBreed = pet.PetBreed;
            existingPet.PetSex = pet.PetSex;
            existingPet.PetDateOfBirth = pet.PetDateOfBirth;
            existingPet.PetDistinguishingFeatures = pet.PetDistinguishingFeatures;
            existingPet.PetPicture = pet.PetPicture;

            context.Pet.Update(existingPet);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("User/{userId}/Pets/Delete/{petId}")]
        public async Task<IActionResult> DeletePet(string userId, int petId)
        {
            int clientId = context.ClientUser.Where(c => c.Id == userId).Select(c => c.ClientId).FirstOrDefault();
            var clientPet = await context.ClientPet.FirstOrDefaultAsync(cp => cp.ClientId == clientId && cp.PetId == petId);

            if (clientPet == null)
            {
                return NotFound();
            }

            context.ClientPet.Remove(clientPet);

            var pet = await context.Pet.FirstOrDefaultAsync(p => p.PetId == petId);
            if (pet != null)
            {
                pet.PetIsActive = false;
            }

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetPetSpecies")]
        public async Task<ActionResult<IEnumerable<PetSpeciesResourceModel>>> GetSpecies()
        {
            var species = await context.PetSpecies.ToListAsync();

            var petSpeciesModels = species.Select(s => new PetSpeciesResourceModel
            {
                PetSpeciesId = s.PetSpeciesId,
                PetSpeciesName = s.PetSpeciesName,
                PetSpeciesDescription = s.PetSpeciesDescription ?? "",
            }).ToList();

            return petSpeciesModels;
        }

        [HttpGet]
        [Route("GetPetHistory/{petId}")]
        public async Task<ActionResult<List<PetHistory>>> GetPetHistory(int petId)
        {
            return await context.PetHistory
                                 .Where(ph => ph.PetId == petId)
                                 .ToListAsync();
        }

        [HttpPost]
        [Route("AddPetHistory")]
        public async Task<IActionResult> AddPetHistory([FromBody] PetHistoryResourceModel petHistory)
        {
            var newPetHistory = new PetHistory
            {
                PetId = petHistory.PetId,
                PetHistoryTitle = petHistory.PetHistoryTitle,
                PetWeight = petHistory.PetWeight,
                PetHistoryNotes = petHistory.PetHistoryNotes,
                PetHistoryCreatedDate = petHistory.PetHistoryCreatedDate,
                PetHistoryUpdatedDate = petHistory.PetHistoryUpdatedDate,
            };
            context.PetHistory.Add(newPetHistory);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("UpdatePetHistory")]
        public async Task<IActionResult> UpdatePetHistory([FromBody] PetHistoryResourceModel petHistory)
        {
            var existingPetHistory = await context.PetHistory.FindAsync(petHistory.PetHistoryId);
            if (existingPetHistory == null)
            {
                return NotFound();
            }

            existingPetHistory.PetId = petHistory.PetId;
            existingPetHistory.PetHistoryTitle = petHistory.PetHistoryTitle;
            existingPetHistory.PetWeight = petHistory.PetWeight;
            existingPetHistory.PetHistoryNotes = petHistory.PetHistoryNotes;
            existingPetHistory.PetHistoryCreatedDate = petHistory.PetHistoryCreatedDate;
            existingPetHistory.PetHistoryUpdatedDate = petHistory.PetHistoryUpdatedDate;

            context.PetHistory.Update(existingPetHistory);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("DeletePetHistory/{petHistoryId}")]
        public async Task<IActionResult> DeletePetHistory(int petHistoryId)
        {
            var petHistory = await context.PetHistory.FirstOrDefaultAsync(ph => ph.PetHistoryId == petHistoryId);

            if (petHistory == null)
            {
                return NotFound();
            }

            context.PetHistory.Remove(petHistory);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
