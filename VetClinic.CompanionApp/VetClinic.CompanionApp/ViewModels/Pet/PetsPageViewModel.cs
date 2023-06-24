using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.Views;
using VetClinic.CompanionApp.Views.Pet;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Pet
{
    public class PetsPageViewModel : BaseViewModel
    {
        public PetDataStore PetDataStore => DependencyService.Get<PetDataStore>();
        public Command AddPetCommand { get; set; }

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
        public PetsPageViewModel()
        {
            LoadPets();
            AddPetCommand = new Command(async () => await OnAddPet());
            PetDataStore.PetAdded += async (s, e) => await LoadPets();
        }

        public async Task LoadPets()
        {
            var pets = await PetDataStore.GetPetsForUser();
            Pets = new ObservableCollection<PetModel>(pets);
        }

        private async Task OnAddPet()
        {
            DependencyService.RegisterSingleton<PetModel>(new PetModel());
            await Shell.Current.GoToAsync("AddOrEditPetPage");
        }
        public void OnPetTapped(PetModel petModel)
        {
            DependencyService.RegisterSingleton<PetModel>(petModel);
            Shell.Current.GoToAsync("PetDetailsPage");
        }
    }

}
