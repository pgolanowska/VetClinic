using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinic.Data.Data.Staff
{
    public class Title
    {
        [Key]
        public int TitleId { get; set; }

        [Required(ErrorMessage = "Abbreviation is required")]
        [MaxLength(10, ErrorMessage = "Abbreviation cannot exceed 10 chars")]
        [Display(Name = "Title Abbreviation")]
        public string TitleAbbrev { get; set; }

        [Required(ErrorMessage = "Title name is required")]
        [MaxLength(40, ErrorMessage = "Title name cannot exceed 40 chars")]
        [Display(Name = "Title Name")]
        public string TitleName { get; set; }

        [Column(TypeName = "bit")]
        public bool TitleIsActive { get; set; }
        public List<Employee> Employee { get; set; }

        public Title()
        {
            this.Employee = new List<Employee>();
        }
    }
}
