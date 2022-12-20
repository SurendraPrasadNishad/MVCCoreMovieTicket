using OnlineMovieTicket.Data.Base.Repository;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.Services.producer
{
    public class ProducersService:EntityBaseRepository<Producer>,IProducersService
    {
        public ProducersService(ApplicationDBContext context):base(context)
        {
                
        }
    }
}
