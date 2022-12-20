using OnlineMovieTicket.Data.Base.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Cinema Logo")]
        [Required(ErrorMessage = "Cinema Logo is Required")]

        public string CinemaLogo { get; set; }
        [Display(Name = "Cinema Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = " Cinema Name must be Between 3 to 50 character")]
       [Required(ErrorMessage = "Cinema Name  is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Cinema Desciption  is Required")]
        [Display(Name = "Description")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Cinema Description  must be Between 3 to 150 character")]
        public string Description { get; set; }
        //Relation 1-M

        public List<Movie> Movies { get; set; }

    }
}
