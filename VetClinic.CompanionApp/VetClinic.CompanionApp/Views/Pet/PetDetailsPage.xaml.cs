using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.ViewModels.Pet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views.Pet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetDetailsPage : ContentPage
    {
        PetDetailsViewModel viewModel;
        public PetDetailsPage()
        {
            InitializeComponent();
            var pageService = DependencyService.Get<IPageService>();
            this.BindingContext = viewModel = new PetDetailsViewModel(pageService);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel?.GetPetHistory();
        }
        void OnPetHistoryEntrySelected(object sender, ItemTappedEventArgs e)
        {
            var petHistory = (PetHistoryModel)e.Item;
            viewModel.OnPetHistoryEntryTapped(petHistory);
        }
    }
}