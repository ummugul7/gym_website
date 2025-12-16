using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proje.Models
{
   
    public class Coach
    {
        public int Id { get; set; } 
        public string UserId { get; set; }

        [Required]
       public string speciality { get; set; }
        public int experience { get; set; }

        public Member member { get; set; }

        public int price { get; set; }
        public ICollection<Appointment> Appointments { get; set; } 
    }
}
