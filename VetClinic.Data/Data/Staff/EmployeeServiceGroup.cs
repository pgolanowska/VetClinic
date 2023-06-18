using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Data.Data.Clinic;

namespace VetClinic.Data.Data.Staff
{
    public class EmployeeServiceGroup
    {
        [Key]
        public int EmployeeServiceGroupId { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ServiceGroupId { get; set; }
        public ServiceGroup ServiceGroup { get; set; }
    }
}
