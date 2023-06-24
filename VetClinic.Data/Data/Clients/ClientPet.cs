using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Data.Data.Clients
{
    public class ClientPet
    {
        [Key]
        public int ClientPetId { get; set; }
        public int PetId { get; set; }
        public Pet? Pet { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
