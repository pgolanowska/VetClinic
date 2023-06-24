using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	public partial class AddOrEditPetHistoryPage : ContentPage
	{
        public AddOrEditPetHistoryPage()
		{
			InitializeComponent();
			this.BindingContext = new AddOrEditPetHistoryViewModel();
		}
    }
}