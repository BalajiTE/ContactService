using EvolentContacts.Helpers;
using System.ComponentModel.DataAnnotations;

namespace EvolentContacts.Models
{
    [RequriedEitherValidator("Email", "Phone", ErrorMessage = "Either Email or Phone Data is Required")]
    public class EvolentContact : EntityBase
    {
        [Display(Name="First Name")]
        [StringLength(50, ErrorMessage = "First Name cannot Exceed 50 Character length")]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last Name cannot Exceed 50 Character length")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [StringLength(150)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }

        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }

    }
}