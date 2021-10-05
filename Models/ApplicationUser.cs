using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.Models
{
    public class ApplicationUser: IdentityUser
    {
        /* Using identity to capture the user information so i had to add 
        first and last name to the database
        This model inherits from the user tables created in the database
        use this to add additional fields to the database.*/

        public string FirstName { get; set; }
        //public Byte[] Image { get; set; }
        public string LastName { get; set; }
        public int SchoolId { get; set; }
        public string ConfirmPassword { get; set; }
    }

   

}
