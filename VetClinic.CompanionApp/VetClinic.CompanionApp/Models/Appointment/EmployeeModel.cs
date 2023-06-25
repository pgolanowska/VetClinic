using System;
using System.Collections.Generic;
using System.Text;

namespace VetClinic.CompanionApp.Models.Appointment
{
    public class EmployeeModel
    {
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeTitle { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeeEducation { get; set; }
        public string EmployeeBio { get; set; }
        public byte[] EmployeePhoto { get; set; }
        public List<string> EmployeeServiceGroups { get; set; }
        public string DisplayNameAndSurname
        {
            get
            {
                return $"{EmployeeName} {EmployeeSurname}";
            }
        }
    }
}
