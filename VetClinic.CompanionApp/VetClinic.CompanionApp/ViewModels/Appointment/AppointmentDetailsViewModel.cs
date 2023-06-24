using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using VetClinic.CompanionApp.Models.Appointment;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Services;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Appointment
{
    public class AppointmentDetailsViewModel : BaseViewModel
    {
        protected readonly IPageService _pageService;
        public AppointmentDataStore AppointmentDataStore => DependencyService.Get<AppointmentDataStore>();
        private AppointmentModel _appointment;
        public AppointmentModel Appointment
        {
            get { return _appointment; }
            set { _appointment = value; OnPropertyChanged(nameof(Appointment)); }
        }
        public Command EditAppointmentCommand { get; set; }
        public Command CancelAppointmentCommand { get; set; }
        public AppointmentDetailsViewModel(IPageService pageService)
        {
            _pageService = pageService;
            Appointment = DependencyService.Get<AppointmentModel>();
            EditAppointmentCommand = new Command(EditAppointment);
            CancelAppointmentCommand = new Command(CancelAppointment);
        }

        public async void EditAppointment()
        {
            await Shell.Current.GoToAsync("EditAppointmentPage");
        }
        public async void CancelAppointment()
        {
            var confirm = await _pageService.DisplayAlert("Confirm Cancel", "Are you sure you want to cancel this appointment?", "Yes", "No");
            if (confirm)
            {
                await AppointmentDataStore.CancelAppointment(Appointment.AppointmentId);
                await _pageService.DisplayAlert("Appointment Canceled", "Your appointment has been canceled.", "OK");
                await Shell.Current.GoToAsync("//AppointmentsPage");
            }
        }
    }
}
