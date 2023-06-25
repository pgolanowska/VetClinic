using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VetClinic.Data.Data.Staff;

namespace VetClinic.WebAPI.ResourceModels
{
    public class EmployeeResourceModel
    {
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeTitle { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeeEducation { get; set; }
        public string EmployeeBio { get; set; }
        public byte[] EmployeePhoto { get; set; }
        public List<string> EmployeeServiceGroups { get; set; }
    }
}
