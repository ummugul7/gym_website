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
                        experience = model.experience
                    };

                    dbContext.Coach.Add(coach);
                    await dbContext.SaveChangesAsync();

                    CreateWeeklyAppointments(coach.Id);

                    TempData["Mesaj"] = "Eğitmen başarıyla kaydedildi.";
                    return RedirectToAction("Index" ,"Home"); 
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);  
        }


        public IActionResult ListCoach()
        {
            var coaches = dbContext.Coach
          .Include(c => c.member)
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

        private void CreateWeeklyAppointments(int coachId)
        {
            var startDate = DateTime.Today;
            var appointments = new List<Appointment>();

            for (int day = 0; day < 7; day++)
            {
                var date = startDate.AddDays(day);

                // Her gün için 9:00 - 18:00 arası saatlik slotlar
                for (int hour = 9; hour <= 17; hour++)
                {
                    appointments.Add(new Appointment
                    {
                        Date = date,
                        Time = new TimeSpan(hour, 0, 0),
                        CoachId = coachId,
                        IsBooked = false,
                        MemberId = null
                    });
                }
            }

            dbContext.Appointment.AddRange(appointments);
            dbContext.SaveChanges();
        }

        public IActionResult Edit(int id)
        {
            TempData["id"] = id;
            return View();
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

            coach.member.UserName = coachView.UserName;
            coach.member.Email = coachView.Email;

            dbContext.SaveChanges();
            TempData["Mesaj"] = "Edit successfully.";
            return RedirectToAction("ListCoach");
        }



       
    }
}
