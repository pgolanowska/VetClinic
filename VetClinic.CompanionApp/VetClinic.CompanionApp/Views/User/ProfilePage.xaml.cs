using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.ViewModels.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private readonly ProfileViewModel _viewModel;

        public ProfilePage()
        {
            InitializeComponent();
            var pageService = DependencyService.Get<IPageService>();
            BindingContext = new ProfileViewModel(pageService);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as ProfileViewModel).GetProfile();
        }
    }
}