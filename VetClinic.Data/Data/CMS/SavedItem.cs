using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Data.Data.Staff;

namespace VetClinic.Data.Data.CMS
{
    public class SavedItem
    {

        [Key]
        public int SavedItemId { get; set; }
        public string SessionId { get; set; }

        [Column(TypeName = "bit")]
        public bool IsAppointment { get; set; }
        [Display(Name = "Appointment Date")]
        public DateTime? AppointmentDateTime { get; set; }
        [Display(Name = "Doctor's Name")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
