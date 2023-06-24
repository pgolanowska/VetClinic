using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.ViewModels.Pet;

namespace VetClinic.CompanionApp.Services
{
    public class PetDataStore : BaseDataStore
    {
        public delegate void PetAddedEventHandler(object sender, EventArgs e);
        public event PetAddedEventHandler PetAdded;
        public delegate void PetHistoryAddedEventHandler(object sender, EventArgs e);
        public event PetHistoryAddedEventHandler PetHistoryAdded;
        public async Task<List<PetModel>> GetPetsForUser()
        {
            var response = await _client.GetAsync(_apiUrl + "User/" + UserId + "/Pets");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var pets = JsonConvert.DeserializeObject<List<PetModel>>(responseContent);
                return pets;
            }
            return null;
        }
        public async Task UpdatePet(PetModel pet)
        {
            var content = new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_apiUrl + "User/" + UserId + "/Pets/Update", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating pet");
            }
        }
        public async Task AddPet(PetModel pet)
        {
            var content = new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apiUrl + "User/" + UserId + "/Pets/Add", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error adding pet");
            }
            PetAdded?.Invoke(this, new EventArgs());
        }
        public async Task DeletePet(int petId)
        {
            var response = await _client.DeleteAsync(_apiUrl + "User/" + UserId + "/Pets/Delete/" + petId);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error deleting pet");
            }
        }
        public async Task<List<PetSpeciesModel>> GetPetSpeciesAsync()
        {
            var response = await _client.GetAsync(_apiUrl + "GetPetSpecies");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var petSpecies = JsonConvert.DeserializeObject<List<PetSpeciesModel>>(responseContent);
                return petSpecies;
            }
            return null;
        }

        public async Task<List<PetHistoryModel>> GetPetHistoryAsync(int petId)
        {
            var response = await _client.GetAsync(_apiUrl + "GetPetHistory/" + petId);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PetHistoryModel>>(responseContent);
            }
            return new List<PetHistoryModel>();
        }
        public async Task AddPetHistoryAsync(PetHistoryModel petHistory)
        {
            var content = new StringContent(JsonConvert.SerializeObject(petHistory), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apiUrl + "AddPetHistory", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error adding history");
            }
            PetHistoryAdded?.Invoke(this, new EventArgs());
        }
        public async Task UpdatePetHistoryAsync(PetHistoryModel petHistory)
        {
            var content = new StringContent(JsonConvert.SerializeObject(petHistory), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_apiUrl + "UpdatePetHistory", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating history");
            }
        }

        public async Task DeletePetHistoryAsync(int petHistoryId)
        {
            var response = await _client.DeleteAsync(_apiUrl + "DeletePetHistory/" + petHistoryId);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error deleting pet history");
            }
        }
    }


}
