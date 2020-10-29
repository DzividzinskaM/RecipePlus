using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlus.ViewModels
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Please enter email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is wrong")]
        public string ConfirmPassword { get; set; }
    }
}
