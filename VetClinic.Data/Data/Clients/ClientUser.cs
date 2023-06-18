using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Data.Data.Staff;

namespace VetClinic.Data.Data.Clients
{
    public class ClientUser : IdentityUser
    {
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public ClientUser()
        {
        }
    }
}
