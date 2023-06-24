using System;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.Views.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.RegisterSingleton<UserDataStore>(new UserDataStore());
            DependencyService.RegisterSingleton<PetDataStore>(new PetDataStore());
            DependencyService.RegisterSingleton<AppointmentDataStore>(new AppointmentDataStore());
            DependencyService.Register<IPageService, PageService>();
            MainPage = new LoginPage();
        }

        public static void GoToMainPage()
        {
            Current.MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
