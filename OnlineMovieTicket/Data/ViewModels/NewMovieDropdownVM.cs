
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.ViewModels
{
    public class NewMovieDropdownVM
    {
        // through constructor define that it is of type new List in which value will be store from different table in it using the services and passto Movies/Create view 
        public NewMovieDropdownVM()
        {
            Actors = new List<Actor>();
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
        }
        public  List<Actor> Actors { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }


    }
}
