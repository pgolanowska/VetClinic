using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.Services
{
    public class PageService : IPageService
    {
        public Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }
        public Task DisplayAlert(string title, string message, string ok)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, ok);
        }
    }
}
