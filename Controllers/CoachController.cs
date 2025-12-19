using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using proje.Data;
using proje.Models;

namespace proje.Controllers
{
   
    public class CoachController : Controller
    {
        private UserManager<Member> UserManager; // egitmeni user olarak kayıt ermek için 
        private readonly ApplicationDbContext dbContext;

        public CoachController(UserManager<Member> userManager, ApplicationDbContext context)
        {
            UserManager = userManager;
            dbContext = context;
        }
        public IActionResult Record()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Record(CoachViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = new Member
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                var result = await UserManager.CreateAsync(user, model.Password);  

                if (result.Succeeded)  
                {
                    await UserManager.AddToRoleAsync(user, "Coach"); 

                    var coach = new Coach
                    {
                        UserId = user.Id,
                        speciality = model.speciality,
                        experience = model.experience,
                        price = model.price
                    };

                    dbContext.Coach.Add(coach);
                    await dbContext.SaveChangesAsync();

                    TempData["Mesaj"] = "Coach successfully recorded.";
                    return RedirectToAction("ListCoach"); 
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    TempData["Mesaj"] = "something it's wrong try again .";
                    return RedirectToAction("Index", "Home");

                }
            }

            return View();  
        }


        public IActionResult ListCoach()
        {
            var coaches = dbContext.Coach
          .Include(c => c.member)
          .Include(c => c.Appointments.Where(a => a.IsBooked == true && a.IsConfirmed==true))
          .ToList();

            return View(coaches);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var coach = dbContext.Coach.Find(id);

            if (coach != null)
            {
                string userId = coach.UserId;

                dbContext.Coach.Remove(coach);
                await dbContext.SaveChangesAsync();

                var member = await UserManager.FindByIdAsync(userId);
                if (member != null)
                {
                    await UserManager.DeleteAsync(member);
                    TempData["Mesaj"] = "Coach successfully deleted.";
                }
            }else
            {
                TempData["Mesaj"] = "something it's wrong try again .";
            }
            
            return RedirectToAction("ListCoach");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var coach = dbContext.Coach
                .Include(c => c.member)
                .FirstOrDefault(c => c.Id == id);

            if (coach == null)
            {
                return NotFound();
            }

            var viewModel = new CoachViewModel
            {
                Id = coach.Id,
                UserName = coach.member.UserName,
                Email = coach.member.Email,
                speciality = coach.speciality,
                experience = coach.experience,
                price = coach.price
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CoachViewModel coachView)
        {

            CoachViewModel model = coachView;
            
            ModelState.Remove("Password");
            if (! ModelState.IsValid)
            {
                TempData["Mesaj"] = "something it's wrong try again .";
                return RedirectToAction("ListCoach");

            }
           
          int id = (int)TempData["id"]; // edit den idyi alıyorum 
             


            var coach = dbContext.Coach.Include(c => c.member).FirstOrDefault(c => c.Id == id);

            coach.speciality = coachView.speciality;
            coach.experience = coachView.experience;
            coach.price = coachView.price;

            coach.member.UserName = coachView.UserName;
            coach.member.Email = coachView.Email;

            dbContext.SaveChanges();
            TempData["Mesaj"] = "Edit successfully.";
            return RedirectToAction("ListCoach");
        }


        
        

    }
}
