using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using proje.Data;
using proje.Models;
using System.Security.Claims;

namespace proje.Controllers
{
   
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<Member> userManager;
        private readonly SignInManager<Member> signInManager;

        public MemberController(UserManager<Member> userManager, ApplicationDbContext context, SignInManager<Member> signInManager)
        {
            this.userManager = userManager;
            this.dbContext = context;
            this.signInManager = signInManager;
        }

        public IActionResult MyInformation()
        {
            string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var member = dbContext.Users
                .FirstOrDefault(m => m.Id == memberId);
            return View("MyInformation", member);
        }
        public IActionResult MemberEdit()
        {
            string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var member = dbContext.Users
                .FirstOrDefault(m => m.Id == memberId);
            return View("MemberEdit", member);
        }

        [HttpPost]
        public async Task<IActionResult> MemberEdit(Member model, string currentPassword, string newPassword)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // mevcut kullacının id değerini alıp dbden useri çekip degerlerini güncelliyoruz 
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                if (user.Email != model.Email)
                {
                    var token = await userManager.GenerateChangeEmailTokenAsync(user, model.Email);
                    await userManager.ChangeEmailAsync(user, model.Email, token);
                    await userManager.SetUserNameAsync(user, model.Email);
                }

                if (!string.IsNullOrEmpty(currentPassword) && !string.IsNullOrEmpty(newPassword))
                {  // bu kısımlar identityin özellikeri 
                    var passwordResult = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                    if (!passwordResult.Succeeded)
                    {
                        TempData["Error"] = "Curred password false!";
                        return View(user);
                    }
                }

                user.weight = model.weight;
                user.length = model.length;

                dbContext.SaveChanges();

                TempData["Success"] = "Updated your information succesifuly!";
            }

            return RedirectToAction("MyInformation");
        }
        public IActionResult MyAppointment()
        {
            string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var memberAppointmentList = dbContext.Appointment
                .Include(x => x.Coach)
                    .ThenInclude(c => c.member)
                .Where(x => x.MemberId == memberId && x.Date >= DateTime.Now)
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Time)
                .ToList();
            return View("MyAppointment", memberAppointmentList); 
        }

        [HttpPost]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var appointment = await dbContext.Appointment.FindAsync(id);

            if (appointment != null && appointment.MemberId == User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                appointment.IsBooked = false;
                appointment.MemberId = null;
                appointment.IsConfirmed = true;
                await dbContext.SaveChangesAsync();
                TempData["Msj"] = "Appointment cancelled successfully.";
            }
            return RedirectToAction("MyAppointment");
        }

        public async Task<IActionResult> Delete()
        {
            string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var member = dbContext.Users
                .FirstOrDefault(m => m.Id == memberId);
            if (member != null) {
                var appointments = dbContext.Appointment
                    .Where(a => a.MemberId == memberId)
                    .ToList();
                foreach (var appointment in appointments)
                {
                    appointment.IsBooked = false;
                    appointment.MemberId = null;
                    appointment.IsConfirmed = false;
                }
                
                dbContext.Users.Remove(member);
                await dbContext.SaveChangesAsync();
                await signInManager.SignOutAsync();
                TempData["info"] = "Your account has been deleted.";
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListMembers()
        {
            var members = await userManager.GetUsersInRoleAsync("Member");
            return View(members);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMember(string  Id)
        {
            var member = dbContext.Users.FirstOrDefault(m => m.Id == Id);
            if (member != null)
            {
                var appointments = dbContext.Appointment
                    .Where(a => a.MemberId == Id)
                    .ToList();
                foreach (var appointment in appointments)
                {
                    appointment.IsBooked = false;
                    appointment.MemberId = null;
                    appointment.IsConfirmed = false;
                }

                dbContext.Users.Remove(member);
                await dbContext.SaveChangesAsync();
                TempData["info"] = "Your account has been deleted.";
                return RedirectToAction("ListMembers");
            }
            return RedirectToAction("ListMembers");
        }
    }
}
