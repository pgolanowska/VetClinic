using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinic.Data.Data.Staff
{
    public class Title
    {
        [Key]
        public int TitleId { get; set; }

        [Required(ErrorMessage = "Title name is required")]
        [MaxLength(20, ErrorMessage = "Title name cannot exceed 20 chars")]
        [Display(Name = "Title Name")]
        public string TitleName { get; set; }

        [Column(TypeName = "bit")]
        public bool TitleIsActive { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
