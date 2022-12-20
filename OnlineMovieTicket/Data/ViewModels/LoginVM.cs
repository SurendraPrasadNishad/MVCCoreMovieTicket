using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage = "Email Is Required*")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage ="Password Is Required*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
