using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.ViewModels.Appointment;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views.Appointment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentDetailsPage : ContentPage
    {
        AppointmentDetailsViewModel viewModel;
        public AppointmentDetailsPage()
        {
            InitializeComponent();
            var pageService = DependencyService.Get<IPageService>();
            this.BindingContext = viewModel = new AppointmentDetailsViewModel(pageService);
        }
    }
}