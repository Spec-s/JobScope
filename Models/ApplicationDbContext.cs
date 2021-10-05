using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobScope.Models
{
    public interface IAppDbContextService
    {
         DbSet<ApplicationUser> UsersInformation { get; set; }

        /* these Dbset are being used to create the tables that will
         * be used in the database "I used migration to add these 'Employmenttable'" */

         DbSet<Jobs> Jobs { get; set; }
         DbSet<UserJobs> UserJobs { get; set; }
    }
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>, IAppDbContextService
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): 
            base(options)
        {
            // this function instanciate the Identity User database.
        }

        public DbSet<ApplicationUser> UsersInformation { get; set; }

        /* these Dbset are being used to create the tables that will
         * be used in the database "I used migration to add these 'Employmenttable'" */

        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<UserJobs> UserJobs { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
    

