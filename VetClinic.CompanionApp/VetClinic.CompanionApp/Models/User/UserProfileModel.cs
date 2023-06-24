using System;
using System.Collections.Generic;
using System.Text;

namespace VetClinic.CompanionApp.Models.User
{
    public class UserProfileModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string DisplayNameAndSurname
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
    }
}
