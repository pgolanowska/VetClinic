using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.Appointment;
using VetClinic.CompanionApp.Services;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Appointment
{
    public class EditAppointmentViewModel : BaseViewModel
    {
        public AppointmentDataStore AppointmentDataStore => DependencyService.Get<AppointmentDataStore>();
        private AppointmentModel _appointment;
        public AppointmentModel Appointment
        {
            get { return _appointment; }
            set { _appointment = value; OnPropertyChanged(nameof(Appointment)); }
        }
        public Command SaveChangesCommand { get; set; }
        public EditAppointmentViewModel()
        {
            Appointment = DependencyService.Get<AppointmentModel>();
            SaveChangesCommand = new Command(async () => await UpdateAppointment());
        }
        private async Task UpdateAppointment()
        {
            await AppointmentDataStore.UpdateAppointment(Appointment);
            DependencyService.RegisterSingleton<AppointmentModel>(Appointment);
            await Shell.Current.GoToAsync("//AppointmentsPage");
            await Shell.Current.GoToAsync("AppointmentDetailsPage");
        }
    }
}
