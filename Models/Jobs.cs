using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.Models
{
    public class Jobs: DataRecords
    {
        // all the details for the jobs on the website will be added in this class
       
        public string JobId { get; set; }
        
        public string JobPosition { get; set; }

        public string Location { get; set;}

        public DateTime Updated { get; set; }

        public String Department { get; set; }

        public string JobDescription { get; set; }

    }
}
