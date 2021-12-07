using JobScope.Models;
using JobScope.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GenerateNumber()
        {
            /* random number used with a string to make a unique Identification */

            const string chars = "0123456789";
            int length = 8;
            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length)
               .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

        public async Task<IActionResult> ViewJobs()
        {
            /* this displays the list results for the jibs in the database */

            var listofJobs = await _context.Jobs.ToListAsync();
            var jobs = listofJobs.Select(x => new JobListModel
            {
                Id = x.Id,
                Department = x.Department,
                JobPosition = x.JobPosition,
                Location = x.Location,
                JobDescription = x.JobDescription,
                Updated = x.Updated
            }).ToList();

            return View("ViewJobs", jobs);
        }

        [Authorize(Roles = ("Admin"))]
        public IActionResult CreateJobs()
        {
            return View();
        }

        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> AddJobs(JobsModel model)
        {
            /* used for adding jobs to the database */

            var getCurrentUser = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            string jobidno = DateTime.Now.ToString("yyMMdd") + GenerateNumber();

            var newJobs = new Jobs
            {
                Location = model.Location,
                Department = model.Department,
                JobPosition = model.JobPosition,
                JobId = Guid.NewGuid().ToString(),
                JobDescription = model.JobDescription,
                Updated = DateTime.Now,
                CreatedById = getCurrentUser.Id
            };
            _context.Jobs.Add(newJobs);
            var result = await _context.SaveChangesAsync();
            if (result > 0) return RedirectToAction("ViewJobs", "Jobs");
            return RedirectToAction("CreateJobs", "Jobs");
        }

       /* [Authorize(Roles = ("Admin"))]
        private List<JobListModel> AddJobs()
        {
            throw new NotImplementedException();
        }
       */
      
        public async Task<IActionResult> DeleteJobAsync(Guid Id)
        {
            /* used for deletion of a job */

            var Foundjob = await _context.Jobs.FirstOrDefaultAsync(x =>x.Id == Id);
            if (Foundjob == null) return RedirectToAction("ViewJobs", "Jobs");
            _context.Jobs.Remove(Foundjob);
            var Results = await _context.SaveChangesAsync();
            if (Results > 0) return RedirectToAction("ViewJobs", "Jobs");
            return RedirectToAction("CreateJobs", "Jobs");
        }


        [Authorize(Roles = ("Admin"))]
        [HttpGet]
        public async Task<IActionResult> EditJob(Guid id)
        {
            /* this function is specific to the admins only, this is used to find the specific job in the database and edit it*/

            var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            return View("EditJob", job);
        }

        [HttpPost]
        public async Task<IActionResult> EditJobAsync(Jobs model)
        {
            /* after the jobs has been successfully editted this function will rewrite the changes and save it to the database */

            var editjob = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == model.Id);
            if(editjob != null) return RedirectToAction("ViewJobs", new { id = editjob.Id });
            _context.Jobs.Add(editjob);
            await _context.SaveChangesAsync();
            return RedirectToAction("ViewJobs", "Jobs");
            
        }
        

    }
}
