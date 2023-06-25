using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	public partial class SchedulePage : ContentPage
	{
		ScheduleViewModel viewModel;
		public SchedulePage ()
		{
			InitializeComponent ();
            var pageService = DependencyService.Get<IPageService>();
            this.BindingContext = viewModel = new ScheduleViewModel(pageService);
		}
        void OnDayScheduleTapped(object sender, ItemTappedEventArgs e)
        {
            var daySchedule = (string)e.Item;

            // brakuje jeszcze checku vs. juz zabookowane sloty
            if (daySchedule != "Off"
                && (viewModel.SelectedDate > DateTime.Now.Date
                || (viewModel.SelectedDate == DateTime.Now.Date
                && TimeSpan.Parse(daySchedule) > DateTime.Now.TimeOfDay)))
            {
                var selectedSchedule = (ScheduleModel)((ListView)sender).BindingContext;
                viewModel.Book(selectedSchedule, daySchedule);
            }
            else viewModel.TimeSlotUnavailable();
        }

        void OnDoctorTapped(object sender, ItemTappedEventArgs e)
        {
            var schedule = (ScheduleModel)e.Item;
            viewModel.DoctorDetails(schedule);
        }

    }
}