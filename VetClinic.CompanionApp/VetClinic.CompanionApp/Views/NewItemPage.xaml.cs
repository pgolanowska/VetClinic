using System;
using System.Collections.Generic;
using System.ComponentModel;
using VetClinic.CompanionApp.Models;
using VetClinic.CompanionApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VetClinic.CompanionApp.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}