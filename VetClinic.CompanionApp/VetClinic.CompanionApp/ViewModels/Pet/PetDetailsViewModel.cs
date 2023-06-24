using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.Views.Pet;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Pet
{
    public class PetDetailsViewModel : BaseViewModel
    {
        protected readonly IPageService _pageService;
        private PetModel _pet;
        public PetModel Pet
        {
            get { return _pet; }
            set { SetProperty(ref _pet, value); }
        }
        public PetDataStore PetDataStore => DependencyService.Get<PetDataStore>();
        private ObservableCollection<PetHistoryModel> _petHistoryCollection;
        public ObservableCollection<PetHistoryModel> PetHistoryCollection 
        {
            get { return _petHistoryCollection; }
            set {
                _petHistoryCollection = value;
                OnPropertyChanged(nameof(PetHistoryCollection));
            }
        }
        public Command AddHistoryCommand { get; set; }
        public Command EditPetCommand { get; set; }
        public Command DeletePetCommand { get; set; }
        private PetHistoryModel _latestPetHistory;
        public PetHistoryModel LatestPetHistory
        {
            get { return _latestPetHistory; }
            set { SetProperty(ref _latestPetHistory, value); }
        }

        private decimal? _latestWeight;
        public decimal? LatestWeight
        {
            get { return _latestWeight; }
            set { SetProperty(ref _latestWeight, value); }
        }
        public PetDetailsViewModel(IPageService pageService)
        {
            _pageService = pageService;
            Pet = DependencyService.Get<PetModel>();
            AddHistoryCommand = new Command(OnAddPetHistoryTapped);
            EditPetCommand = new Command(OnEditPetTapped);
            DeletePetCommand = new Command(OnDeletePetTapped);
        }
        public async Task GetPetHistory()
        {
            var petHistoryList = await PetDataStore.GetPetHistoryAsync(Pet.PetId);
            PetHistoryCollection = new ObservableCollection<PetHistoryModel>(petHistoryList);
            LatestPetHistory = PetHistoryCollection.OrderByDescending(h => h.PetHistoryCreatedDate).FirstOrDefault();
            LatestWeight = LatestPetHistory?.PetWeight ?? 0;
        }
        public async void OnAddPetHistoryTapped()
        {
            DependencyService.RegisterSingleton<PetHistoryModel>(new PetHistoryModel());
            await Shell.Current.Navigation.PushAsync(new AddOrEditPetHistoryPage());
            MessagingCenter.Send(this, "latestWeight", LatestWeight.ToString());
        }
        public async void OnEditPetTapped()
        {
            await Shell.Current.GoToAsync("AddOrEditPetPage");
        }
        public async void OnDeletePetTapped()
        {
            var confirm = await _pageService.DisplayAlert("Confirm Delete", "Are you sure you want to delete this pet?", "Yes", "No");
            if (confirm)
            {
                await PetDataStore.DeletePet(Pet.PetId);
                await Shell.Current.GoToAsync("//PetsPage");
            }
        }
        public void OnPetHistoryEntryTapped(PetHistoryModel petHistoryModel)
        {
            DependencyService.RegisterSingleton<PetHistoryModel>(petHistoryModel);
            Shell.Current.GoToAsync("PetHistoryDetailsPage");
        }
        
    }
}
