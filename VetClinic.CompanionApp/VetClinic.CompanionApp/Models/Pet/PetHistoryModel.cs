using System;
using System.Collections.Generic;
using System.Text;

namespace VetClinic.CompanionApp.Models.Pet
{
    public class PetHistoryModel
    {
        public int PetHistoryId { get; set; }
        public int PetId { get; set; }
        public string PetHistoryTitle { get; set; }
        public decimal PetWeight { get; set; }
        public string PetHistoryNotes { get; set; }
        public DateTime PetHistoryCreatedDate { get; set; }
        public DateTime PetHistoryUpdatedDate { get; set; }
        public string DisplayDateAndTitle
        {
            get
            {
                return $"{PetHistoryCreatedDate:dd/MM/yyyy} - {PetHistoryTitle}";
            }
        }
    }
}
