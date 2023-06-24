using System;
using System.Collections.Generic;
using VetClinic.CompanionApp.ViewModels;
using VetClinic.CompanionApp.Views.User;
using VetClinic.CompanionApp.Views;
using Xamarin.Forms;
using VetClinic.CompanionApp.Views.Pet;
using VetClinic.CompanionApp.Views.Appointment;

namespace VetClinic.CompanionApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
           // Routing.RegisterRoute(nameof(PetsPage), typeof(PetsPage));
           // Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(AddOrEditPetPage), typeof(AddOrEditPetPage));
            Routing.RegisterRoute(nameof(PetDetailsPage), typeof(PetDetailsPage));
            Routing.RegisterRoute(nameof(AddOrEditPetHistoryPage), typeof(AddOrEditPetHistoryPage));
            Routing.RegisterRoute(nameof(PetHistoryDetailsPage), typeof(PetHistoryDetailsPage));
            Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
            Routing.RegisterRoute(nameof(ChangePasswordPage), typeof(ChangePasswordPage));
            Routing.RegisterRoute(nameof(AppointmentConfirmationPage), typeof(AppointmentConfirmationPage));
            Routing.RegisterRoute(nameof(SchedulePage), typeof(SchedulePage));
            Routing.RegisterRoute(nameof(AppointmentDetailsPage), typeof(AppointmentDetailsPage));
            Routing.RegisterRoute(nameof(EditAppointmentPage), typeof(EditAppointmentPage));
        }

    }
}
