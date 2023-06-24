using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VetClinic.CompanionApp.Models;
using VetClinic.CompanionApp.Models.User;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.User
{
    public class LoginViewModel : BaseViewModel
    {
        public UserDataStore UserDataStore => DependencyService.Get<UserDataStore>();
        public Command LoginCommand { get; }
        public Command NavigateToRegisterCommand { get; private set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            NavigateToRegisterCommand = new Command(OnRegisterClicked);

        }

        private async void OnLoginClicked(object obj)
        {
            bool result = await Login();
            if (!result)
            {
                Message = "Login failed";
            }
            else
            {
                App.GoToMainPage();
                await Shell.Current.GoToAsync("//PetsPage");
            }
        }
        private async void OnRegisterClicked(object obj)
        {
            App.GoToMainPage();
            await Shell.Current.GoToAsync("RegisterPage");
        }
        public async Task<bool> Login()
        {
            await UserDataStore.LoginAsync(Username, Password);
            return true;
        }
    }
}
