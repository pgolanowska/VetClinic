using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Reflection.Metadata;

namespace VetClinic.Data.Data.CMS
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(20, ErrorMessage = "Title cannot exceed 20 chars")]
        [Display(Name = "Link Title")]
        public string LinkTitle { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(30, ErrorMessage = "Title cannot exceed 30 chars")]
        [Display(Name = "Page Title")]
        public string PageTitle { get; set; }

        [Display(Name = "Content")]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Content { get; set; }

        [Display(Name = "Added On")]
        public DateTime AddedDate { get; set; }

        [Display(Name = "Added By")]
        public string AddedBy { get; set; }

        [Column(TypeName = "bit")]
        public bool NewsIsNotArchived { get; set; }

        public News()
        {
            this.AddedDate = DateTime.Now;
            this.AddedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            this.NewsIsNotArchived = true;
        }
    }
}
