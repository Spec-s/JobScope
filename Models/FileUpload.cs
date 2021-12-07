using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.Models
{
    public class FileUpload : ApplicationUser
    {
        public IFormFile Photo { get; set; }

        public IFormFile Resume { get; set; }

    }
}
