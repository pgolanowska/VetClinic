using System;
using System.Collections.Generic;
using System.Text;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Services;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Pet
{
    public class PetHistoryDetailsViewModel : BaseViewModel
    {
        protected readonly IPageService _pageService;
        private PetHistoryModel _petHistory;
        public PetHistoryModel PetHistory
        {
            get { return _petHistory; }
            set { SetProperty(ref _petHistory, value); }
        }
        public PetDataStore PetDataStore => DependencyService.Get<PetDataStore>();
        public Command EditPetHistoryCommand { get; set; }
        public Command DeletePetHistoryCommand { get; set; }
        public PetHistoryDetailsViewModel(IPageService pageService)
        {
            _pageService = pageService;
            PetHistory = DependencyService.Get<PetHistoryModel>();
            EditPetHistoryCommand = new Command(OnEditPetHistoryTapped);
            DeletePetHistoryCommand = new Command(OnDeletePetHistoryTapped);
        }
        public async void OnEditPetHistoryTapped()
        {
            await Shell.Current.GoToAsync("AddOrEditPetHistoryPage");
        }
        public async void OnDeletePetHistoryTapped()
        {
            var confirm = await _pageService.DisplayAlert("Confirm Delete", "Are you sure you want to delete this entry?", "Yes", "No");
            if (confirm)
            {
                await PetDataStore.DeletePetHistoryAsync(PetHistory.PetHistoryId);
                await Shell.Current.GoToAsync("//PetsPage");
            }
        }
    }
}
