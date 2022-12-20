using OnlineMovieTicket.Data.Base.Repository;
using OnlineMovieTicket.Data.ViewModels;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.Services.movie
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        //For single Movie getting by id Service we can do from here also 
        Task<Movie> GetMovieByIdAsync(int id);

        //For displayng values from multiple table in the dropdown for movie in create form  fetching data
        Task<NewMovieDropdownVM> GetNewMovieDropdownValuesAsync();

        //creating interface to Saving data received from received from Movie create in Movies and Actors_Movies table in database
        Task AddingNewMovieAsync(NewMovieVM data);
        //creating interface to Update data received from received from Movie Edit  in Movies and Actors_Movies table in database
        Task UpdateMovieAsync(int id,NewMovieVM editData);

    }
}
