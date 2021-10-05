using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            //Used to retrieve the List of roles present in the database

            Users = new List<string>();
        }
        public String Id { get; set; }

        [Required(ErrorMessage = "Role Name is Required")]
        public String RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
