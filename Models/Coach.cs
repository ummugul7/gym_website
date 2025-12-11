using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proje.Models
{
    [Table("Egitmenler")]
    public class Coach
    {
        public int Id { get; set; } 
        public string UserId { get; set; }

        [Required]
        public string speciality { get; set; }

        public int experience { get; set; }

        public Member member { get; set; }
    }
}
