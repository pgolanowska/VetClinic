using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new AppShell();
        }
    }
}