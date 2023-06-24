namespace VetClinic.WebAPI.ResourceModels
{
    public class ScheduleResourceModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public byte[] EmployeePhoto { get; set; }
        public List<string> DaySchedule { get; set; }
        public ScheduleResourceModel()
        {
            DaySchedule = new List<string>();
        }
    }
}
