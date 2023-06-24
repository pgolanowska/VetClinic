namespace VetClinic.WebAPI.ResourceModels
{
    public class AppointmentResourceModel
    {
        public int AppointmentId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public int ClientId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string OwnerEmail { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string IssueDescription { get; set; }
        public string Status { get; set; }
    }
}
