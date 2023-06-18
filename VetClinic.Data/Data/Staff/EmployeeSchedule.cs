using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Data.Data.Staff
{
    public class EmployeeSchedule
    {
        [Key]
        public int EmployeeScheduleId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime ScheduleDate { get; set; }
        [Column(TypeName = "bit")]
        public bool IsWorking { get; set; }
        [Display(Name = "Shift Start")]
        public TimeSpan? StartTime { get; set; }
        [Display(Name = "Shift End")]
        public TimeSpan? EndTime { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? Comment { get; set; }
    }
}
