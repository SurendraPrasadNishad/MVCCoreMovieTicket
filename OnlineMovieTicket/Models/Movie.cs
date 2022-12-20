using OnlineMovieTicket.Data.Base.Repository;
using OnlineMovieTicket.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Models
{
    public class Movie: IEntityBase
    {
        [Key]
        public int Id { get; set; }     
        public string MovieName { get; set; }
        public string Description { get; set; }        
        public string Imageurl { get; set; }
        public DateTime StartDate { get; set; }      
        public DateTime EndDate { get; set; }       
        public double Price { get; set; }      
        public MovieCategory MovieCategory { get; set; }
        //Relations M-m
        public List<Actor_Movie> Actors_Movies { get; set; }
        //Relation with  Cinema M-1
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }
        //Relation with producer M-1
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }




    }
}
