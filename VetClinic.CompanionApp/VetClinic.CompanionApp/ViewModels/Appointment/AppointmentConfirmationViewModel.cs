using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.Appointment;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Models.User;
using VetClinic.CompanionApp.Services;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Appointment
{
    public class AppointmentConfirmationViewModel : BaseViewModel
    {
        public ScheduleModel SelectedSchedule { get; }
        public string SelectedTimeSlot { get; }
        public DateTime SelectedDate { get;  }
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
        public UserDataStore UserDataStore => DependencyService.Get<UserDataStore>();
        public PetDataStore PetDataStore => DependencyService.Get<PetDataStore>();
        public AppointmentDataStore AppointmentDataStore => DependencyService.Get<AppointmentDataStore>();

        private ObservableCollection<PetModel> _pets;
        public ObservableCollection<PetModel> Pets
        {
            get { return _pets; }
            set
            {
                _pets = value;
                OnPropertyChanged(nameof(Pets));
            }
        }
        private PetModel _selectedPet;
        public PetModel SelectedPet
        {
            get { return _selectedPet; }
            set
            {
                _selectedPet = value;
                OnPropertyChanged(nameof(SelectedPet));
            }
        }
        private string _issueDescription;
        public string IssueDescription
        {
            get { return _issueDescription; }
            set
            {
                _issueDescription = value;
                OnPropertyChanged(nameof(IssueDescription));
            }
        }
        public Command BookAppointmentCommand { get; set; }
        protected readonly IPageService _pageService;
        public AppointmentConfirmationViewModel(ScheduleModel selectedSchedule, DateTime selectedDate, string selectedTimeSlot, IPageService pageService)
        {
            _pageService = pageService;
            SelectedSchedule = selectedSchedule;
            SelectedDate = selectedDate;
            SelectedTimeSlot = selectedTimeSlot;
            BookAppointmentCommand = new Command(async () => await BookAppointment());
            Initialize();
        }
        public async Task Initialize()
        {
            var pets = await PetDataStore.GetPetsForUser();
            Pets = new ObservableCollection<PetModel>(pets);
            CurrentUserProfile = await UserDataStore.GetProfile();
        }

        public async Task BookAppointment()
        {
            bool isSuccessful = await AppointmentDataStore.AddAppointment(CurrentUserProfile, SelectedSchedule, SelectedDate, SelectedTimeSlot, SelectedPet, IssueDescription);

            if (isSuccessful)
            {
                await _pageService.DisplayAlert("Success", "The appointment has been successfully booked.", "OK");
                await Shell.Current.GoToAsync("//AppointmentsPage");
            }
            else
            {
                await _pageService.DisplayAlert("Error", "There was an error booking the appointment. Please try again.", "OK");
            }
        }
    }
}
