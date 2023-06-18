using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Data.Data.Clinic
{
    public class ClinicScheduleException
    {
        [Key]
        public int ClinicScheduleExceptionId { get; set; }
        public DateTime ScheduleDate { get; set; }
        [Column(TypeName = "bit")]
        public bool IsOpen { get; set; }
        [Display(Name = "Opens At")]
        public TimeSpan? OpenTime { get; set; }
        [Display(Name = "Closes At")]
        public TimeSpan? CloseTime { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Comment { get; set; }
    }
}
