using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.ViewModels
{
    public class RegisterModel
    {
        
        public String FirstName { get; set; }
      
        public int SchoolId { get; set; }  
        public String LastName { get; set; }
               
        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Confirm Password must match.") ]
        public string ConfirmPassword { get; set; }

        // remember to code the confirm password field
    }
}
