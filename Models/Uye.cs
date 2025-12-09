using Microsoft.AspNetCore.Identity;

namespace proje.Models
{
    public class Uye : IdentityUser
    {
        public int kilo {  get; set; }

        public int boy { get; set; } 


    }
}
