using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.Base.Repository
{
    public interface IEntityBase
    {
        //In models suppose all model has common a Id then put here
        int Id {get ;set;}
    }
}
