using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.Appointment;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.ViewModels.Appointment;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views.Appointment
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppointmentConfirmationPage : ContentPage
	{
		public AppointmentConfirmationPage(ScheduleModel selectedSchedule, DateTime selectedDate, string selectedTimeSlot)
        {
            InitializeComponent();
            var pageService = DependencyService.Get<IPageService>();
            BindingContext = new AppointmentConfirmationViewModel(selectedSchedule, selectedDate, selectedTimeSlot, pageService);
        }
    }
}