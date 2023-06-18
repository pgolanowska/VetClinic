using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Data.Data.Staff;

namespace VetClinic.Data.Data.Clinic
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDateTime { get; set; }
        [Display(Name = "Owner's Name")]
        public string OwnerName { get; set; }
        [Display(Name = "Owner's Phone")]
        public string OwnerPhoneNumber { get; set; }
        [Display(Name = "Owner's E-mail")]
        public string OwnerEmail { get; set; }
        [Display(Name = "Pet's Name")]
        public string PetName { get; set; }
        [Display(Name = "Issue Description")]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string IssueDescription { get; set; }
        [Column(TypeName = "bit")]
        public bool IsConfirmed { get; set; }
        [Column(TypeName = "bit")]
        public bool HasArrived { get; set; }
        [Column(TypeName = "bit")]
        public bool IsCompleted { get; set; }
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        public Appointment()
        {
            this.IsConfirmed = false;
            this.IsActive = true;
            this.HasArrived = false;
            this.IsCompleted = false;
            this.AppointmentDateTime = DateTime.Now;
        }
    }
}
