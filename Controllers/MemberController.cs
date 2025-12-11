using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;
using proje.Models;

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
            return View();
        }

    
    }
}
