using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;
using proje.Models;
using System.Security.Claims;

namespace proje.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AppointmentController( ApplicationDbContext context)
        {
            dbContext = context;
        }
        [Authorize(Roles = "Member")]
        public IActionResult AppointmentSelectCoach()
        {
            var coaches = dbContext.Coach
            .Include(c => c.member)
            .ToList();

            return View(coaches);
        }

        // bana bir tane coach id gelecek 

        public IActionResult AppointmentList(int id)
        {
            var groupedAppointments = dbContext.Appointment
         .Where(x => x.CoachId == id && x.IsBooked == false && x.Date >= DateTime.Now )  // burada seçilen koca göre ve aktif tarihe göre ranfevusu olamaynlar ılisteliyor 
         .GroupBy(x => x.Date.Date)     // Tarihe göre grupla
         .Select(g => new AppointmentGroupVM
         {
             Date = g.Key,
             Times = g.Select(a => new AppointmentTimeVM
             {
                 AppointmentId = a.Id,
                 Time = a.Time
             }).ToList()
         })
         .ToList();

            return View(groupedAppointments);
        }

      
       public async Task<IActionResult> AppointmentBook(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var appointment = await dbContext.Appointment.FindAsync(id);
            if (appointment == null)  //
            {
                TempData["Mesaj"] = "something it's wrong try again ." + id ; // appointmentid 0 geliyor 

                return RedirectToAction("AppointmentList");
            }
         

            appointment.MemberId = userId;   // burada set ediyorsun
            appointment.IsBooked = true;     // randevu dolu oldu

            await dbContext.SaveChangesAsync();

            TempData["Mesaj"] = "Appointment successfull";

            return RedirectToAction("AppointmentList", new { id = appointment.CoachId }); 
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

