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
    public class InfoPage
    {
        [Key]
        public int InfoPageId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(20, ErrorMessage = "Title cannot exceed 20 chars")]
        [Display(Name = "Link Title")]
        public string InfoLinkTitle { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(30, ErrorMessage = "Title cannot exceed 30 chars")]
        [Display(Name = "Page Title")]
        public string InfoPageTitle { get; set; }

        [Display(Name = "Content")]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string InfoContent { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime InfoLastEdited { get; set; }

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }

        public InfoPage()
        {
            this.IsActive = true;
        }
    }
}
