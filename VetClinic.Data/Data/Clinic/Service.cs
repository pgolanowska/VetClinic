using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VetClinic.Data.Data.Staff;

namespace VetClinic.Data.Data.Clinic
{
    public class Service : TEntity
    {
        [Key]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 20 chars")]
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [Display(Name = "Service Description")]
        [Column(TypeName = "nvarchar(max)")]
        public string ServiceDesc { get; set; }

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        public int ServiceGroupId { get; set; }
        public ServiceGroup? ServiceGroup { get; set; }

        public Service()
        {
            this.IsActive = true;
        }
    }
}
