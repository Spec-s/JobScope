using JobScope.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.ViewModels
{
    public class JobsModel
    {
        public string JobId { get; set; }
        
        public string JobPosition { get; set; }
        
        public string Location { get; set; }
        
        public DateTime Updated { get; set; }
        
        public String Department { get; set; }
        
        public string JobDescription { get; set; }

        public List<JobListModel> JobList { get; set; }
    }
}
