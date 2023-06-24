using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace VetClinic.CompanionApp.Services
{
    public class BaseDataStore
    {
        public static HttpClient _client = new HttpClient();
        public string _apiUrl = "https://localhost:7272/";
        private static string _userId;
        public static string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
            }
        }
        public BaseDataStore() { }
    }
}
