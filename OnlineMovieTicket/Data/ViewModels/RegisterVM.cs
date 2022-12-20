using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.ViewModels
{
    public class RegisterVM
    {

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name Is Required*")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password Is Required*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirmation of Password Is Required*")]
        [Compare("Password",ErrorMessage ="Password didnt matched , Check Password ")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Is Required*")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

    }
}
