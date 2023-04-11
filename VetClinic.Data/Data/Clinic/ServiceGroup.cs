using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VetClinic.Data.Data.Staff;

namespace VetClinic.Data.Data.Clinic
{
    public class ServiceGroup
    {
        [Key]
        public int ServiceGroupId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 20 chars")]
        [Display(Name = "Service Name")]
        public string ServiceGroupName { get; set; }

        [Display(Name = "Service Description")]
        [Column(TypeName = "nvarchar(max)")]
        public string ServiceGroupDesc { get; set; }

        [Column(TypeName = "bit")]
        public bool ServiceGroupIsActive { get; set; }

        public List<Position> Positions { get; set; }

        public ServiceGroup()
        {
            this.Positions = new List<Position>();
        }
    }
}
