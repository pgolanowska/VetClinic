using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Models.User;
using VetClinic.CompanionApp.Services;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.User
{
    public class EditProfileViewModel : BaseViewModel
    {
        private UserProfileModel _userProfile;
        public UserProfileModel CurrentUserProfile
        {
            get { return _userProfile; }
            set
            {
                _userProfile = value;
                OnPropertyChanged(nameof(CurrentUserProfile));
            }
        }
        public Command SaveCommand { get; set; }
        public Command ChangePasswordCommand { get; set; }
        public UserDataStore UserDataStore => DependencyService.Get<UserDataStore>();
        public EditProfileViewModel()
        {
            CurrentUserProfile = DependencyService.Get<UserProfileModel>();
            SaveCommand = new Command(async () => await SaveProfileChanges());
            ChangePasswordCommand = new Command(async () => await OnChangePasswordTapped());
        }
        private async Task SaveProfileChanges()
        {
            CurrentUserProfile.Address = CurrentUserProfile.Address ?? string.Empty;
            CurrentUserProfile.PhoneNumber = CurrentUserProfile.PhoneNumber ?? string.Empty; // profesjonalne rozwiązanie ;)
            await UserDataStore.UpdateProfileAsync(CurrentUserProfile);
            await Shell.Current.GoToAsync("//ProfilePage");
        }

        private async Task OnChangePasswordTapped()
        {
            await Shell.Current.GoToAsync("ChangePasswordPage");
        }
    }
}
