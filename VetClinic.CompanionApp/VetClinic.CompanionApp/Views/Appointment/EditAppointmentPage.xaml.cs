using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.ViewModels.Appointment;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views.Appointment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAppointmentPage : ContentPage
    {
        public EditAppointmentPage()
        {
            InitializeComponent();
            this.BindingContext = new EditAppointmentViewModel();
        }
    }
}