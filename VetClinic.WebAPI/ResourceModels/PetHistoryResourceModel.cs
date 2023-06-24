namespace VetClinic.WebAPI.ResourceModels
{
    public class PetHistoryResourceModel
    {
        public int PetHistoryId { get; set; }
        public int PetId { get; set; }
        public string PetHistoryTitle { get; set; }
        public decimal PetWeight { get; set; }
        public string PetHistoryNotes { get; set; }
        public DateTime PetHistoryCreatedDate { get; set; }
        public DateTime PetHistoryUpdatedDate { get; set; }
    }
}
