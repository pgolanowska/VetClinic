using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.Appointment;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Services;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Appointment
{
    public class AppointmentsViewModel : BaseViewModel
    {
        public Command GetAppointmentHistoryCommand { get; set; }
        public Command GetUpcomingAppointmentsCommand { get; set; }
        public Command BookNewAppointmentCommand { get; set; }
        private ObservableCollection<AppointmentModel> _appointments;
        public ObservableCollection<AppointmentModel> Appointments
        {
            get { return _appointments; }
            set { _appointments = value; OnPropertyChanged(nameof(Appointments)); }
        }
        public AppointmentDataStore AppointmentDataStore => DependencyService.Get<AppointmentDataStore>();
        public AppointmentsViewModel()
        {
            BookNewAppointmentCommand = new Command(async () => await BookNewAppointment());
            GetUpcomingAppointmentsCommand = new Command(async () => await GetUpcomingAppointments());
            GetUpcomingAppointments();
        }

        public async Task GetUpcomingAppointments()
        {
            var appointments = await AppointmentDataStore.GetUpcomingAppointments();
            Appointments = new ObservableCollection<AppointmentModel>(appointments);
        }

        public async Task BookNewAppointment()
        {
            await Shell.Current.GoToAsync("SchedulePage");
        }
        public void OnAppointmentTapped(AppointmentModel appointmentModel)
        {
            DependencyService.RegisterSingleton<AppointmentModel>(appointmentModel);
            Shell.Current.GoToAsync("AppointmentDetailsPage");
        }
    }
}
