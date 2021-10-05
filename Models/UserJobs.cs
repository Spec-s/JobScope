using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.Models
{
    public class UserJobs: DataRecords
    {
        /* many to many... this will inherit from the data records and will use 
          foreign keys to pull the data */
        // this method allows the database tables to be cross referenced and can easily locate 
        // any applicant who applied for a job.

        public Guid UserId { get; set; }

        public Guid JobId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Jobs JobListings { get; set; }
    }
    
}
