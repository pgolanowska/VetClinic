using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.BusinessLogic;
using VetClinic.CompanionApp.Models.Appointment;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Models.User;

namespace VetClinic.CompanionApp.Services
{
    public class AppointmentDataStore : BaseDataStore
    {
        public async Task<List<ScheduleModel>> GetSchedule(DateTime selectedDate)
        {
            string date = selectedDate.ToString("yyyy-MM-dd");
            var response = await _client.GetAsync(_apiUrl + "GetSchedule/" + date);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var schedules = JsonConvert.DeserializeObject<List<ScheduleModel>>(responseContent);
                return schedules;
            }
            return null;
        }
        public async Task<bool> AddAppointment(UserProfileModel currentUserProfile, ScheduleModel selectedSchedule, DateTime selectedDate, string selectedTimeSlot, PetModel selectedPet, string issueDescription)
        {
            AppointmentModel appointment = new AppointmentModel {
                AppointmentId = -1,
                EmployeeId = selectedSchedule.EmployeeId,
                EmployeeName = selectedSchedule.EmployeeName,
                EmployeeSurname = selectedSchedule.EmployeeName,
                AppointmentDateTime = selectedDate.Date + TimeSpan.Parse(selectedTimeSlot),
                OwnerName = currentUserProfile.DisplayNameAndSurname,
                OwnerPhoneNumber = currentUserProfile.PhoneNumber,
                OwnerEmail = currentUserProfile.Email,
                PetId = selectedPet.PetId,
                PetName = selectedPet.PetName,
                IssueDescription = issueDescription
            };
            var content = new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apiUrl + "BookAppointment/" + UserId, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error booking appointment");
            }
            AppointmentManager appointmentManager = new AppointmentManager();
            appointmentManager.ScheduleAppointmentReminder(appointment);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<AppointmentModel>> GetUpcomingAppointments()
        {
            var response = await _client.GetAsync(_apiUrl + "User/" + UserId + "/Appointments/Upcoming");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var appointments = JsonConvert.DeserializeObject<List<AppointmentModel>>(responseContent);
                return appointments;
            }
            return null;
        }
        public async Task CancelAppointment(int appointmentId)
        {
            var response = await _client.GetAsync(_apiUrl + "CancelAppointment/" + appointmentId);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error canceling appointment");
            }
        }

        public async Task UpdateAppointment(AppointmentModel appointment)
        {
            var content = new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_apiUrl + "UpdateAppointment", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating appointment");
            }
        }

        public async Task<EmployeeModel> GetEmployeeDetails(int id)
        {
            var response = await _client.GetAsync(_apiUrl + "GetEmployeeDetails/" + id);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var employee = JsonConvert.DeserializeObject<EmployeeModel>(responseContent);
                return employee;
            }
            return null;
        }
    }
}
