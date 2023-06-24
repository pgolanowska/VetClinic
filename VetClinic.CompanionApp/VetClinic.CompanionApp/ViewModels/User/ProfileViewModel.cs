using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Models.User;
using VetClinic.CompanionApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace VetClinic.CompanionApp.ViewModels.User
{
    public class ProfileViewModel : BaseViewModel
    {
        protected readonly IPageService _pageService;
        public UserDataStore UserDataStore => DependencyService.Get<UserDataStore>();
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
        public Command EditProfileCommand { get; set; }
        public Command DeleteAccountCommand { get; set; }

        public ProfileViewModel(IPageService pageService)
        {
            _pageService = pageService;
            CurrentUserProfile = new UserProfileModel();
            EditProfileCommand = new Command(OnEditProfileTapped);
            DeleteAccountCommand = new Command(OnDeleteAccountTapped);
        }
        public async Task GetProfile()
        {
            CurrentUserProfile = await UserDataStore.GetProfile();
        }
        public async void OnEditProfileTapped()
        {
            DependencyService.RegisterSingleton<UserProfileModel>(CurrentUserProfile);
            await Shell.Current.GoToAsync("EditProfilePage");
        }
        public async void OnDeleteAccountTapped()
        {
            var confirm = await _pageService.DisplayAlert("Confirm Delete", "Are you sure you want to delete your account?", "Yes", "No");
            if (confirm)
            {
                //await PetDataStore.DeletePet(Pet.PetId);
                //await Shell.Current.GoToAsync("//PetsPage");
            }
        }
    }

}
