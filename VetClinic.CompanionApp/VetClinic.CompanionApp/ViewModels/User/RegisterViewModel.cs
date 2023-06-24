using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VetClinic.CompanionApp.Models.User;
using VetClinic.CompanionApp.Services;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.User
{
    public class RegisterViewModel : BaseViewModel
    {
        public UserDataStore UserDataStore => DependencyService.Get<UserDataStore>();
        public Command RegisterCommand { get; }
        public Command NavigateToLoginCommand { get; private set; }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set 
            { 
               _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await Register());
            NavigateToLoginCommand = new Command(async () => await Shell.Current.GoToAsync("LoginPage"));
        }

        private async Task Register()
        {
            var registerModel = new RegisterModel
            {
                Email = Email,
                Password = Password,
                Name = Name,
                Surname = Surname
            };
            await UserDataStore.Register(registerModel);
            App.GoToMainPage();
            await Shell.Current.GoToAsync("//ProfilePage");
        }
    }
}

