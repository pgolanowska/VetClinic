using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using VetClinic.CompanionApp.Models.Appointment;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.Views.Appointment;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Appointment
{
    public class ScheduleViewModel : BaseViewModel
    {
        public AppointmentDataStore AppointmentDataStore => DependencyService.Get<AppointmentDataStore>();
        public ObservableCollection<ScheduleModel> Schedule { get; set; }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                    LoadSchedule();
                }
            }
        }
        private ScheduleModel _selectedSchedule;
        public ScheduleModel SelectedSchedule
        {
            get { return _selectedSchedule; }
            set
            {
                if (_selectedSchedule != value)
                {
                    _selectedSchedule = value;
                    OnPropertyChanged(nameof(SelectedSchedule));
                }
            }
        }
        private string _selectedTime;
        public string SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                if (_selectedTime != value)
                {
                    _selectedTime = value;
                    OnPropertyChanged(nameof(SelectedTime));
                }
            }
        }
        public Command IncreaseDateCommand { get; private set; }
        public Command DecreaseDateCommand { get; private set; }
        protected readonly IPageService _pageService;
        public ScheduleViewModel(IPageService pageService)
        {
            _pageService = pageService;
            SelectedDate = DateTime.Today;
            SelectedTime = string.Empty;

            IncreaseDateCommand = new Command(IncreaseDate);
            DecreaseDateCommand = new Command(DecreaseDate);

            LoadSchedule();
        }

        private async void LoadSchedule()
        {
            // słabo, że za każdym razem pobierają się zdjęcia, do rozbicia na 1x pobieranie pracowników i osobno schedule'a
            var schedule = await AppointmentDataStore.GetSchedule(SelectedDate);
            Schedule = new ObservableCollection<ScheduleModel>(schedule);
            OnPropertyChanged(nameof(Schedule));
        }
        private void IncreaseDate()
        {
            SelectedDate = SelectedDate.AddDays(1);
        }

        private void DecreaseDate()
        {
            SelectedDate = SelectedDate.AddDays(-1);
        }

        public async void Book(ScheduleModel selectedSchedule, string selectedTimeSlot)
        {
            await Application.Current.MainPage.Navigation.PushAsync(
                new AppointmentConfirmationPage(selectedSchedule, SelectedDate, selectedTimeSlot));
        }
        public async void TimeSlotUnavailable()
        {
            await _pageService.DisplayAlert("Slot Unavailable", "This time slot is not available, sorry this is handled so terribly :(", "OK");
        }

        public async void DoctorDetails(ScheduleModel selectedSchedule)
        {
            DependencyService.RegisterSingleton<ScheduleModel>(selectedSchedule);
            await Shell.Current.GoToAsync("DoctorDetailsPage");
        }
    }
}
