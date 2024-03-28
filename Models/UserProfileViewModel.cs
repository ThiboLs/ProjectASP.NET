using System.ComponentModel.DataAnnotations;

namespace ProjectASP.Models
{
    public class UserProfileViewModel
    {
        [Required]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Familienaam")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Telefoonnummer")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }

}
