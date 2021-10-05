using JobScope.Models;
using JobScope.ViewModels;
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
            const string chars = "0123456789";
            int length = 8;
            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length)
               .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

        public async Task<IActionResult> ViewJobs()
        {
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

        public IActionResult CreateJobs()
        {
            return View();
        }
        public async Task<IActionResult> AddJobs(JobsModel model)
        {
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

        private List<JobListModel> AddJobs()
        {
            throw new NotImplementedException();
        }

      

        public async Task<IActionResult> DeleteJobAsync(Guid Id)
        {
            var Foundjob = await _context.Jobs.FirstOrDefaultAsync(x =>x.Id == Id);
            if (Foundjob == null) return RedirectToAction("ViewJobs", "Jobs");
            _context.Jobs.Remove(Foundjob);
            var Results = await _context.SaveChangesAsync();
            if (Results > 0) return RedirectToAction("ViewJobs", "Jobs");
            return RedirectToAction("CreateJobs", "Jobs");
        }

        [HttpGet]
        public async Task<IActionResult> EditJobAsync(string Id)
        {
            var editjob = await _context.Jobs.FindAsync(Id);
            if(editjob == null)
            {
                return NotFound();
            }

            return View(editjob);
        }


        //public async Task<IActionResult> EditJob(Guid id)
        //{

        //}      
    }
}
