using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.ViewModels.Pet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views.Pet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetsPage : ContentPage
    {
        PetsPageViewModel viewModel;
        public PetsPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new PetsPageViewModel();
        }
        void OnPetSelected(object sender, ItemTappedEventArgs e)
        {
            var pet = (PetModel)e.Item;
            viewModel.OnPetTapped(pet);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadPets();
        }

    }
}