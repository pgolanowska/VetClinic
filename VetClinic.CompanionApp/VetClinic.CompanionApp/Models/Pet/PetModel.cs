using System;
using System.Collections.Generic;
using System.Text;

namespace VetClinic.CompanionApp.Models.Pet
{
    public class PetModel
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public int PetSpeciesId { get; set; }
        public string PetSpecies { get; set; }
        public string PetBreed { get; set; }
        public string PetSex { get; set; }
        public DateTime PetDateOfBirth { get; set; }
        public string PetDistinguishingFeatures { get; set; }
        public byte[] PetPicture { get; set; }
        public PetModel()
        {
            PetDateOfBirth = DateTime.Now;
        }
    }
}
