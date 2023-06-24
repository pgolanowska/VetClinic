using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Data.Data.Clients
{
    public class PetHistory
    {
        [Key]
        public int PetHistoryId { get; set; }
        public int PetId { get; set; }
        public Pet? Pet { get; set; }
        public string PetHistoryTitle { get; set; }
        public decimal? PetWeight { get; set; }
        public string? PetHistoryNotes { get; set; }
        public DateTime PetHistoryCreatedDate { get; set; }
        public DateTime PetHistoryUpdatedDate { get; set; }
        public PetHistory()
        {
            PetHistoryCreatedDate = DateTime.Now;
            PetHistoryUpdatedDate = DateTime.Now;
        }
    }
}
