using System;
using System.Collections.Generic;
using System.Text;

namespace VetClinic.CompanionApp.Models.Appointment
{
    public class ScheduleModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public byte[] EmployeePhoto { get; set; }
        public List<string> DaySchedule { get; set; }
        public ScheduleModel()
        {
            DaySchedule = new List<string>();
        }
    }
}
