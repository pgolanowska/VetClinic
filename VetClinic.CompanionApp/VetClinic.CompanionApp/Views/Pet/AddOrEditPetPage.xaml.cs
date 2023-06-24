using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.ViewModels.Pet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views.Pet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrEditPetPage : ContentPage
    {
        public AddOrEditPetPage()
        {
            InitializeComponent();
            this.BindingContext = new AddOrEditPetViewModel();
        }
    }
}