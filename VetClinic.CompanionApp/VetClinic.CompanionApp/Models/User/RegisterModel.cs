using System;
using System.Collections.Generic;
using System.Text;

namespace VetClinic.CompanionApp.Models.User
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
