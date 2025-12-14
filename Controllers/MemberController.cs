using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;
using proje.Models;
using System.Security.Claims;

namespace proje.Controllers
{
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public MemberController( ApplicationDbContext context)
        {
       
            dbContext = context;
        }

        /*    public IActionResult Myİnformation()
           {
               string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

               var memberAppointmentList = dbContext.Appointment
                   .Include(x => x.Coach)
                       .ThenInclude(c => c.member)
                   .Where(x => x.MemberId == memberId && x.Date >= DateTime.Now)
                   .OrderBy(x => x.Date)
                   .ThenBy(x => x.Time)
                   .ToList();



               return View();
           } */

        public IActionResult Myİnformation()
        {
            string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var memberAppointmentList = dbContext.Appointment
                .Include(x => x.Coach)
                    .ThenInclude(c => c.member)
                .Where(x => x.MemberId == memberId && x.Date >= DateTime.Now)
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Time)
                .ToList();

            // DEBUG

            return View("Myİnformation", memberAppointmentList); // Yeni view adı
        }

        [HttpPost]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var appointment = await dbContext.Appointment.FindAsync(id);

            if (appointment != null && appointment.MemberId == User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                appointment.IsBooked = false;
                appointment.MemberId = null;
                await dbContext.SaveChangesAsync();
                TempData["Mesaj"] = "Appointment cancelled successfully.";
            }

            return RedirectToAction("Myİnformation");
        }
    }
}
