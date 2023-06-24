using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Data.Data.Data;

namespace VetClinic.Data.Data.Clients
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 20 chars")]
        [Display(Name = "Pet's Name")]
        public string PetName { get; set; }
        public int PetSpeciesId { get; set; }
        public PetSpecies PetSpecies { get; set; }
        public string? PetBreed { get; set; }
        public string PetSex { get; set; }
        [Display(Name = "Pet's Date of Birth")]
        public DateTime PetDateOfBirth { get; set; }
        [Display(Name = "Pet's Unique Features")]
        public string? PetDistinguishingFeatures { get; set; }
        [Display(Name = "Pet's Picture")]
        public byte[]? PetPicture { get; set; }
        [Column(TypeName = "bit")]
        public bool PetIsActive { get; set; }
        public Pet()
        {
            PetIsActive = true;
        }
    }
}
