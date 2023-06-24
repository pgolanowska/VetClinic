using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace VetClinic.Data.Data.Clients
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 20 chars")]
        [Display(Name = "Client's Name")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(30, ErrorMessage = "Surname cannot exceed 30 chars")]
        [Display(Name = "Client's Surname")]
        public string ClientSurname { get; set; }

        [Display(Name = "Client's Address")]
        public string? ClientAddress { get; set; }

        [MaxLength(30, ErrorMessage = "Phone number cannot exceed 30 chars")]
        [Display(Name = "Client's Phone Number")]
        public string? ClientPhoneNumber { get; set; }

        [Required(ErrorMessage = "E-mail is required")]
        [Display(Name = "Client's E-mail")]
        public string ClientEmail { get; set; }

        [Column(TypeName = "bit")]
        public bool ClientIsActive { get; set; }
        public Client()
        {
            ClientIsActive = true;
        }
    }
}
