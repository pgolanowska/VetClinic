using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Services;
using VetClinic.CompanionApp.ViewModels.Pet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views.Pet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PetHistoryDetailsPage : ContentPage
	{
        public PetHistoryDetailsPage()
		{
			InitializeComponent();
            var pageService = DependencyService.Get<IPageService>();
            this.BindingContext = new PetHistoryDetailsViewModel(pageService);
		}
	}
}