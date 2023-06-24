using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.Views.Pet;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Pet
{
    public class AddOrEditPetHistoryViewModel : BaseViewModel
    {
        public bool IsEditMode { get; set; }
        private PetModel _pet;
        public PetModel Pet
        {
            get { return _pet; }
            set { SetProperty(ref _pet, value); }
        }
        private PetHistoryModel _petHistory;
        public PetHistoryModel PetHistory
        {
            get { return _petHistory; }
            set { SetProperty(ref _petHistory, value); }
        }
        private decimal _latestWeight;
        public decimal LatestWeight
        {
            get { return _latestWeight; }
            set { _latestWeight = value; OnPropertyChanged(nameof(LatestWeight)); }
        }
        public ICommand AddPetHistoryCommand { get; }
        public PetDataStore PetDataStore => DependencyService.Get<PetDataStore>();
        public AddOrEditPetHistoryViewModel()
        {
            AddPetHistoryCommand = new Command(async () => await AddPetHistory());
            Pet = DependencyService.Get<PetModel>();
            InitializeDataAsync();
        }
        ~AddOrEditPetHistoryViewModel()
        {
            MessagingCenter.Unsubscribe<PetDetailsViewModel, string>(this, "latestWeight");
        }

        private async Task InitializeDataAsync()
        {
            PetHistory = DependencyService.Get<PetHistoryModel>();

            if (PetHistory.PetHistoryTitle == null)
            {
                IsEditMode = false;
                PetHistory = new PetHistoryModel();
                PetHistory.PetId = Pet.PetId;
                PetHistory.PetHistoryCreatedDate = DateTime.Now;
                MessagingCenter.Subscribe<PetDetailsViewModel, string>(this, "latestWeight", (obj, weight) =>
                {
                    LatestWeight = decimal.Parse(weight);
                });
            }
            else
            {
                IsEditMode = true;
                LatestWeight = PetHistory.PetWeight;
            }
            PetHistory.PetHistoryUpdatedDate = DateTime.Now;
        }
        private async Task AddPetHistory()
        {
            PetHistory.PetWeight = LatestWeight;
            if (IsEditMode)
            {
                await PetDataStore.UpdatePetHistoryAsync(PetHistory);
                await Shell.Current.GoToAsync("//PetsPage");
                await Shell.Current.GoToAsync("PetDetailsPage");
                await Shell.Current.GoToAsync("PetHistoryDetailsPage");
            }
            else
            {
                await PetDataStore.AddPetHistoryAsync(PetHistory);
                await Shell.Current.GoToAsync("PetDetailsPage");
            }
        }
    }

}
