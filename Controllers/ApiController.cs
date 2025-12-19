using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proje.Data;
using proje.Models; 

namespace proje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly UserManager<Member> userManager;


        public ApiController(ApplicationDbContext dbcontext, UserManager<Member> userManager)
        {
           this.dbcontext = dbcontext;
           this.userManager = userManager;
        }

        [HttpGet("emails")]
        public async Task<IActionResult> GetMemberEmails()
        {
            var members = await userManager.GetUsersInRoleAsync("Member");
            var emails = members.Select(m => new { m.Email }).ToList();


            if (emails == null || !emails.Any())
            {
                return NotFound("member not found  ");
            }

            return Ok(emails); 
        }
    }
}