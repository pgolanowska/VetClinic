using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Data.Data
{
    public class TEntity
    {

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
    }
}