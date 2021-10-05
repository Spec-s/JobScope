using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.ViewModels
{
    public class LoginModel
    {
        [Required]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }

        [Display(Name = "Remember Me")] 
        public Boolean RememberMe { get; set; }
    }
}
