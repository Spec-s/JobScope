using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.ViewModels
{
    public class RolesModel
    {
        [Required]
        public String RoleName { get; set; }
    }
}
