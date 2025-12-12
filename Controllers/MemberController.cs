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

        public IActionResult Myİnformation()
        {
            string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // anlık olarak sistemde login olan kişinin idsini gönderir 
            var memberAppointmentList = dbContext.Appointment.Where(x=>x.MemberId == memberId && x.Date >= DateTime.Now).GroupBy(x => x.Date.Date).ToList();
            return View("PersonalData",memberAppointmentList);
        }

    
    }
}
