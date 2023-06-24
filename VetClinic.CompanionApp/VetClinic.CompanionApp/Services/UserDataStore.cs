using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.User;

namespace VetClinic.CompanionApp.Services
{
    public class UserDataStore : BaseDataStore
    {
        public UserDataStore()
        {

            if (_apiUrl.Contains("localhost"))
            {
                _client.DefaultRequestHeaders.Add("Connection", "close");
                _client.DefaultRequestHeaders.Add("Accept", "application/json");
                _client.DefaultRequestHeaders.Add("User-Agent", "VetClinic.CompanionApp");

                HttpClientHandler _clientHandler = new HttpClientHandler();
                _clientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                _client = new HttpClient(_clientHandler);
            }
        }

        public async Task LoginAsync(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new { Username = username, Password = password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_apiUrl + "Login", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);
                UserId = loginResponse.UserId;
            }
        }

        public async Task<UserProfileModel> GetProfile()
        {
            var response = await _client.GetAsync(_apiUrl + "User/GetUser/" + UserId);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var profile = JsonConvert.DeserializeObject<UserProfileModel>(responseContent);
                return profile;
            }
            return null;
        }

        public async Task UpdateProfileAsync(UserProfileModel userProfileModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(userProfileModel), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_apiUrl + "User/UpdateUser/" + UserId, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating profile");
            }
        }

        public async Task Register(RegisterModel registerModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_apiUrl + "Register", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);
                UserId = loginResponse.UserId;
            }
            else
            {
                throw new Exception("Registration failed");
            }
        }
        public async Task ChangePasswordAsync(string currentPassword, string newPassword)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { currentPassword, newPassword }), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_apiUrl + "User/ChangePassword/" + UserId, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error changing password");
            }
        }
    }

}
