using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace VetClinic.Data.Data.Staff
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 20 chars")]
        [Display(Name = "Employee's Name")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(30, ErrorMessage = "Surname cannot exceed 30 chars")]
        [Display(Name = "Employee's Surname")]
        public string EmployeeSurname { get; set; }

        [Display(Name = "Employee's Title")]
        public int TitleId { get; set; }
        public Title? Title { get; set; }

        [Display(Name = "Employee's Position")]
        public int PositionId { get; set; }
        public Position? Position { get; set; }

        [MaxLength(100, ErrorMessage = "Education info cannot exceed 100 chars")]
        [Display(Name = "Employee's Education")]
        public string EmployeeEducation { get; set; }

        [Display(Name = "Employee's Bio")]
        [Column(TypeName = "nvarchar(max)")]
        public string EmployeeBio { get; set; }

        [Display(Name = "Employee's Photo")]
        [Column(TypeName = "varbinary(max)")]
        public byte[] EmployeePhoto { get; set; }

        [Column(TypeName = "bit")]
        public bool EmployeeIsActive { get; set; }
        public List<EmployeeServiceGroup>? EmployeeServiceGroups { get; set; }
        public Employee()
        {
            this.EmployeeIsActive = true;
            this.EmployeeServiceGroups = new List<EmployeeServiceGroup>();
        }
    }
}
