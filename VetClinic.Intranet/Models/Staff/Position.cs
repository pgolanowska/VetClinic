using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using VetClinic.Intranet.Models.Clinic;

namespace VetClinic.Intranet.Models.Staff
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Position name is required")]
        [MaxLength(50, ErrorMessage = "Position name cannot exceed 50 chars")]
        [Display(Name = "Position Name")]
        public string PositionName { get; set; }

        [Column(TypeName = "bit")]
        public bool PositionIsActive { get; set; }
        public List<Employee> Employees { get; set; }
        public List<ServiceGroup> ServiceGroups { get; set; }
        public Position()
        {
            this.ServiceGroups = new List<ServiceGroup>();
        }
    }
}
