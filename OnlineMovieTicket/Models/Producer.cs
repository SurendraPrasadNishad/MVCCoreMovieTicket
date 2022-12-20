using OnlineMovieTicket.Data.Base.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Profile Picture")]
        [Required(ErrorMessage ="Profile picture is Required")]
        public string ProfilePictureUrl { get; set; }
      
        [Display(Name ="Producer Name")]
        [Required(ErrorMessage = "Full Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be Between 3 to 50 character")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography Name is Required")]
        [StringLength(250, MinimumLength = 30, ErrorMessage = "Biography must be Between 30 to 250 character")]

        public string Bio { get; set; }
        //Relation 1-M
        public List<Movie> Movies { get; set; }
    }
}
