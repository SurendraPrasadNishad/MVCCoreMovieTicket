using OnlineMovieTicket.Data.Base;
using OnlineMovieTicket.Data.Base.Repository;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.Services.cinema
{
    public class CinemasService:EntityBaseRepository<Cinema>,ICinemasService
    {
        public CinemasService(ApplicationDBContext context):base(context)
        {

        }
    }
}
