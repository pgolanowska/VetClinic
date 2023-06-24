using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Services;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.User
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        public Command ChangePasswordCommand { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }
        public UserDataStore UserDataStore => DependencyService.Get<UserDataStore>();
        public ChangePasswordViewModel()
        {
            ChangePasswordCommand = new Command(async () => await ChangePassword());
        }
        private async Task ChangePassword()
        {
            if (NewPassword != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match!";
                return;
            }
            await UserDataStore.ChangePasswordAsync(CurrentPassword, NewPassword);
            await Shell.Current.GoToAsync("//ProfilePage");
        }
    }
}
