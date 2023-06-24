using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.Appointment;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.ViewModels.Appointment;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views.Appointment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentsPage : ContentPage
    {
        AppointmentsViewModel viewModel;
        public AppointmentsPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new AppointmentsViewModel();
        }
        void OnAppointmentSelected(object sender, ItemTappedEventArgs e)
        {
            var appointment = (AppointmentModel)e.Item;
            viewModel.OnAppointmentTapped(appointment);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetUpcomingAppointments();
        }
    }
}