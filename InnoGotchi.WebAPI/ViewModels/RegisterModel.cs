using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InnoGotchi.WebAPI.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "First Name not specified")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name not specified")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public byte[]? Avatar { get; set; }

    
    }
}
