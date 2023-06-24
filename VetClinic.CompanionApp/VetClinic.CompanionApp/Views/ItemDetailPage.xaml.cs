using System.ComponentModel;
using VetClinic.CompanionApp.ViewModels;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}