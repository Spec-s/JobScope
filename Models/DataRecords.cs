using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.Models
{
    public class DataRecords
    {
     // Guid is a unique string perfect for masking Identification

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public String CreatedById { get; set; }

        public Boolean IsDeleted { get; set; } = false;

        public virtual ApplicationUser CreatedBy { get; set; }

        public DataRecords()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
    }
}
