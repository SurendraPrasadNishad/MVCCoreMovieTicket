using OnlineMovieTicket.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.ViewModels
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Display(Name = "Movie Name")]
        [Required(ErrorMessage = "Movie Name  is Required")]
        public string MovieName { get; set; }
        [Display(Name ="Description")]
        [Required(ErrorMessage = "Movie Description   is Required")]
        [StringLength(250, MinimumLength = 30, ErrorMessage = "Movie must be Between 30 to 250 character")]

        public string Description { get; set; }
        [Display(Name = "Movie Poster")]
        [Required(ErrorMessage = "Movie poster is Required")]
        public string Imageurl { get; set; }
        [Required(ErrorMessage = "Start Date  is Required")]
        [Display(Name = "Movie Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date  is Required")]
        [Display(Name = "Movie End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Movie Price")]
        [Required(ErrorMessage = "Ticket Price in $ Required")]
        public double Price { get; set; }
        [Display(Name = "Movie Category")]
        [Required(ErrorMessage = "Moive Category  is Required")]
        public MovieCategory MovieCategory { get; set; }
        //Relations
        [Display(Name = "Select Movie Actor(s)")]
        [Required(ErrorMessage = "Moive Actor(s)  is Required")]
        public IEnumerable<int> ActorIds { get; set; }
        [Display(Name = "Select Cinema")]
        [Required(ErrorMessage = "Cinema is Required")]
        public int CinemaId { get; set; }
        [Display(Name = "Select Movie Producer(s)")]
        [Required(ErrorMessage = "Moive Producer(s)  is Required")]
        public int ProducerId { get; set; }

    }
}
