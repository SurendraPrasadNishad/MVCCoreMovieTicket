using Microsoft.EntityFrameworkCore;
using OnlineMovieTicket.Data.Base.Repository;
using OnlineMovieTicket.Data.ViewModels;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.Services.movie
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        //for use here we are defining and assing value to constructor and using here
        private readonly ApplicationDBContext _context;
        public MoviesService(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
        // Saving data received from received from Movie create form in the  Movies and Actors_Movies table in database

        public async Task AddingNewMovieAsync(NewMovieVM data)
        {//saving data in Movies table
            var movie = new Movie()
            {
                MovieName=data.MovieName,
                Imageurl=data.Imageurl,
                Description=data.Description,
                MovieCategory=data.MovieCategory,
                Price=data.Price,
                StartDate=data.StartDate,
                EndDate = data.EndDate,
                CinemaId=data.CinemaId,
                ProducerId=data.ProducerId
            };
            _context.Movies.Add(movie);  //from here we get Movie Id of newly  added movie
            await _context.SaveChangesAsync();
            //saving Data in Actors_Movies table since there might be multiple actor in 1 Movie so using loop
            foreach(var act_Item in data.ActorIds)
            {
                var actor_Movie = new Actor_Movie()
                {//Movie id we get as wesave data from above move when we have saved in table
                    MovieId = movie.Id,
                    //ActorId we get from data there may be many actors in 1 Movie 
                    ActorId=act_Item,
                };
                _context.Actors_Movies.Add(actor_Movie);
            }
            await _context.SaveChangesAsync();

        }



        //for single service which is not possible to implement in base repoitory we can do here
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var moviesData = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
            return moviesData;
        }

        public async Task<NewMovieDropdownVM> GetNewMovieDropdownValuesAsync()
        {
            var MovieDropdowndata = new NewMovieDropdownVM()
            {
                Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(p => p.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(a => a.Name).ToListAsync()
            };
            return MovieDropdowndata;
        }

        public async Task UpdateMovieAsync(int id, NewMovieVM editData)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == editData.Id);
            if(dbMovie != null)
            {//Replacing old value with new value and storing in dbMovie
                dbMovie.MovieName = editData.MovieName;
                dbMovie.Imageurl = editData.Imageurl;
                dbMovie.Description = editData.Description;
                dbMovie.MovieCategory = editData.MovieCategory;
                dbMovie.Price = editData.Price;
                dbMovie.StartDate = editData.StartDate;
                dbMovie.EndDate = editData.EndDate;
                dbMovie.CinemaId = editData.CinemaId;
                dbMovie.ProducerId = editData.ProducerId;
            }           
            await _context.SaveChangesAsync();

            //Fetching ActorId ,MovieId  and Removing ActorId from Actors_Movies Table
                var previousData=  _context.Actors_Movies.Where(am => am.MovieId == editData.Id);
                _context.Actors_Movies.RemoveRange(previousData);
                await _context.SaveChangesAsync();
            //Adding updated ActorId and MovieId in table Actors_Movies since 1 Movie has multiple ActorId so using loop
            foreach(var actItem in editData.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId =editData.Id,
                    ActorId=actItem
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();

        }
    }
    }


