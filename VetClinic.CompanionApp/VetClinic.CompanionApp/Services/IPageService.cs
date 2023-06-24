using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.CompanionApp.Services
{
    public interface IPageService
    {
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task DisplayAlert(string title, string message, string ok);
    }
}
