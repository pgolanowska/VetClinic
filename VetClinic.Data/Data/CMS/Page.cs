using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VetClinic.Data.Data.CMS
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }

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
        public bool IsActive { get; set; }

        public Page()
        {
            this.AddedDate = DateTime.Now;
            this.AddedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            this.IsActive = true;
        }
    }
}
