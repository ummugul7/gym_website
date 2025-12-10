using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proje.Models
{
    // neden bu sınıfı kullandım ? 
    // ben egitmenlerimi de Identity ile gelen tabloda saklamak istiyorum ama isim e-maili tekrar egitmen sınıfımda tanımlayamazdım 
    // bu yuzden bu sınıfa ihtiyaç duydum.

    public class CoachViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "speciality ")]
        public string speciality { get; set; }

        [Required]
        [Display(Name = "experience ")]
        public int experience { get; set; }
    }
}
