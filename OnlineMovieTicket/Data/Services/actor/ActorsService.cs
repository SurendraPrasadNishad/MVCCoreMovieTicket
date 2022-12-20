using Microsoft.EntityFrameworkCore;
using OnlineMovieTicket.Data.Base.Repository;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.Services.actor
{
    public class ActorsService :EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(ApplicationDBContext context):base(context)
        {
            
        }
    }
}
