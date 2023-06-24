using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Helpers;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Pet
{
    public class AddOrEditPetViewModel : BaseViewModel
    {
        public bool IsEditMode { get; set; }
        public PetDataStore PetDataStore => DependencyService.Get<PetDataStore>();
        public Command SaveCommand { get; set; }
        public Command SelectImageCommand { get; set; }

        private ImageSource _image;
        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        private PetModel _pet;

        public PetModel Pet
        {
            get { return _pet; }
            set { SetProperty(ref _pet, value); }
        }
        private ObservableCollection<PetSpeciesModel> _petSpeciesCollection;
        public ObservableCollection<PetSpeciesModel> PetSpeciesCollection
        {
            get { return _petSpeciesCollection; }
            set
            {
                _petSpeciesCollection = value;
                OnPropertyChanged(nameof(PetSpeciesCollection));
            }
        }

        private PetSpeciesModel _selectedSpecies;
        public PetSpeciesModel SelectedSpecies
        {
            get { return _selectedSpecies; }
            set
            {
                _selectedSpecies = value;
                OnPropertyChanged(nameof(SelectedSpecies));
            }
        }
        private ImageSource _displayedImage;
        public ImageSource DisplayedImage
        {
            get { return _displayedImage; }
            set
            {
                _displayedImage = value;
                OnPropertyChanged(nameof(DisplayedImage));
            }
        }
        private string _saveButtontext;
        public string SaveButtonText
        {
            get { return _saveButtontext; }
            set { _saveButtontext = value; OnPropertyChanged(nameof(SaveButtonText)); }
        }
        public AddOrEditPetViewModel()
        {
            SaveCommand = new Command(async () => await AddPet());
            SelectImageCommand = new Command(async () => await SelectImage());
            SelectedSpecies = new PetSpeciesModel();
            InitializeDataAsync();
        }
        private async Task AddPet()
        {
            Pet.PetSpeciesId = SelectedSpecies.PetSpeciesId;
            if(IsEditMode)
            {
                await PetDataStore.UpdatePet(Pet);
                await Shell.Current.GoToAsync("//PetsPage");
                await Shell.Current.GoToAsync("PetDetailsPage");
            }
            else
            {
                await PetDataStore.AddPet(Pet);
                await Shell.Current.GoToAsync("//PetsPage");
            }
        }
        private async Task InitializeDataAsync()
        {
            Pet = DependencyService.Get<PetModel>();
            var petSpeciesList = await PetDataStore.GetPetSpeciesAsync();
            PetSpeciesCollection = new ObservableCollection<PetSpeciesModel>(petSpeciesList);
            
            if (Pet.PetName == null)
            {
                Pet = new PetModel();
                IsEditMode = false;
                SaveButtonText = "Add Pet";
            }
            else
            {
                IsEditMode = true;
                SelectedSpecies = PetSpeciesCollection.Where(s => s.PetSpeciesId == Pet.PetSpeciesId).FirstOrDefault();
                if (Pet.PetPicture != null) DisplayedImage = ImageSource.FromStream(() => new MemoryStream(Pet.PetPicture));
                SaveButtonText = "Save Changes";
            }
        }
        private async Task SelectImage()
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            using (var memoryStream = new MemoryStream())
            {
                await photo.OpenReadAsync().Result.CopyToAsync(memoryStream);
                Pet.PetPicture = memoryStream.ToArray();
            }

            var stream = await photo.OpenReadAsync();
            DisplayedImage = ImageSource.FromStream(() => stream);
        }
    }
}
