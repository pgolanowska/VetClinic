using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Data.Data.Data
{
    public class PetSpecies
    {
        [Key]
        public int PetSpeciesId { get; set; }
        public string PetSpeciesName { get; set; }
        public string? PetSpeciesDescription { get; set; }
        [Column(TypeName = "bit")]
        public bool PetSpeciesIsActive { get; set; }
        public PetSpecies()
        {
            PetSpeciesIsActive = true;
        }
    }
}
