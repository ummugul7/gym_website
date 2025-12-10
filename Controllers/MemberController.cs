using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace proje.Controllers
{
    public class MemberController : Controller
    {
        [Authorize(Roles = "Uye")]
        public IActionResult Appointment()
        {
            return View();
        }

        public IActionResult Myİnformation()
        {
            return View();
        }
    }
}
