using Microsoft.AspNetCore.Identity;

namespace proje.Models
{
    public class Member : IdentityUser
    {
        public int length {  get; set; }

        public int weight { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
