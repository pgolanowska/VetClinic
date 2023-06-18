using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinic.Data.Data.Clinic
{
    public class Info
    {
        [Key]
        public int InfoId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(20, ErrorMessage = "Title cannot exceed 20 chars")]
        [Display(Name = "Title")]
        public string InfoTitle { get; set; }


        [Required(ErrorMessage = "Description is required")]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Description")]
        public string InfoDesc { get; set; }
        public bool InfoIsActive { get; set; }

        public Info()
        {
            this.InfoIsActive = true;
        }
    }
}
