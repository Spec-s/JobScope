using JobScope.Models;
using JobScope.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobScope.Controllers
{
  
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Logout()
        {
            /* this function is used to log out the user once they have been fully registered and signed in successfully */

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            /* this function is used to login the user after registration */

            try
            {

                if (ModelState.IsValid)
                {

                    var user = await _context.UsersInformation.FirstOrDefaultAsync(x => x.UserName == model.Email && x.ConfirmPassword == model.Password);
                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(String.Empty, "Invalid password or user name");

                    return View("Login", model);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(model);
        }

        public async Task<IActionResult> CreateUser(RegisterModel registerModel)
        {
            /* this function is used to create a new user in the database */

            try
            {
                var user = new ApplicationUser
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    UserName = registerModel.Email,
                    SchoolId = registerModel.SchoolId,
                    Email = registerModel.Email,
                    ConfirmPassword = registerModel.ConfirmPassword,
                    PasswordHash = registerModel.Password,

                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded) return RedirectToAction("Login", "Account");
                foreach (var error in result.Errors)
                {
                    ViewBag.Error = error.Description;
                }
                return RedirectToAction("RegisterUser", "Account");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> Profile(string id)
        {
            /* this code is to display the current user profile */

            var CurrentUser = await _context.ApplicationUsers.FindAsync(id);
            if(CurrentUser == null)
            {
                return NotFound();
            }
            return RedirectToAction("Profile", "Account");
            
        }

        public async Task<IActionResult> EditProfile(string id)
        {
            /* this is used to edit the current user profile and update with new records */

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            return View("EditProfile", user);
        }

        public async Task<IActionResult> JobApplied(Guid id)
        {
            /* this finds the job which the user applied for */

            var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            return View("ViewJobs", job);
        }
    }
}
