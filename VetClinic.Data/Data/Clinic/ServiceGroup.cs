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

        public List<Position> Position { get; set; }

        [Display(Name = "Service Group Icon")]
        public string ServiceGroupIconName { get; set; }
        public List<Service> Services { get; set; }
        public List<EmployeeServiceGroup> EmployeeServiceGroups { get; set; }

        public ServiceGroup()
        {
            this.Position = new List<Position>();
            this.Services = new List<Service>();
            this.EmployeeServiceGroups = new List<EmployeeServiceGroup>();
            this.ServiceGroupIsActive = true;
        }
    }
}
