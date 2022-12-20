using OnlineMovieTicket.Data.Base.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Models
{
    public class Actor:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Profile Picture")]
        [Required(ErrorMessage ="Profile Picture is Required")]
        public string ProfilePictureUrl { get; set; }

        [Required(ErrorMessage = "Full Name is Required")]
        [Display(Name="Name")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Name must be Between 3 to 50 character")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Biography is Required")]
        [StringLength(250, MinimumLength = 30, ErrorMessage = "Biography must be Between 30 to 250 character")]
        [Display(Name="Biography")]
        public string Bio { get; set; }
        //Relation M-M

        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
