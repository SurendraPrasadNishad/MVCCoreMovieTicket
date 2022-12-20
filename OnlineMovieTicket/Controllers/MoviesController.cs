using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineMovieTicket.Data;
using OnlineMovieTicket.Data.Services.movie;
using OnlineMovieTicket.Data.Static;
using OnlineMovieTicket.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service=service;
        }
        //GetAll :Movies/Index
        //Here we need Movie and Cinema so passing data as parameter to Repository
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var all_mov_Data = await _service.GetAllAsync(m=>m.Cinema);
            return View(all_mov_Data);
        }
        //GetById : Movies/Details/id
        //Here we need Movie and other table data so passing id of movie as parameter to service layer
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
          var mov_Data= await _service.GetMovieByIdAsync(id);
            if (mov_Data == null)
                return View("NotFound");
            else
            return View(mov_Data);
        }
        //Get: Movies/Create
        // Using service it return to the form with dropdown data in the form taking Multiple data different table using ViewModels/NewMovieDropdownVM
        public async Task<IActionResult> Create()
        {
            var formDropdownData = await _service.GetNewMovieDropdownValuesAsync();
            ViewBag.Cinemas = new SelectList(formDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(formDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(formDropdownData.Actors, "Id", "FullName");
            return View();
        }

        //Saving data in Movie database through Service
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM newMovie)
        {
            if (ModelState.IsValid)
            {
                await _service.AddingNewMovieAsync(newMovie);
                ModelState.Clear();
                return RedirectToAction(nameof(Index));
            }
            else
            {//Going to Create form For Movie with Dropdown data 
                var formDropdownData = await _service.GetNewMovieDropdownValuesAsync();

                ViewBag.Cinemas = new SelectList(formDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(formDropdownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(formDropdownData.Actors, "Id", "FullName");
                return View(newMovie);
            }
             
        }
        //Get: Movies/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            //Here we need Movie and other table data so passing id of movie as parameter to service layer
            var mov_Data= await _service.GetMovieByIdAsync(id);
            if (mov_Data == null)
                return View("NotFound");
            //putting data in NewMovieVM by creating object of NewMovieVM taking from fetched data present in mov_Data
                var  oldMovieData = new NewMovieVM()
                {
                    Id = mov_Data.Id,
                    MovieName = mov_Data.MovieName,
                    Imageurl = mov_Data.Imageurl,
                    MovieCategory = mov_Data.MovieCategory,
                    Price = mov_Data.Price,
                    StartDate = mov_Data.StartDate,
                    EndDate = mov_Data.EndDate,
                    Description = mov_Data.Description,
                    ProducerId = mov_Data.ProducerId,
                    CinemaId = mov_Data.CinemaId,
                    ActorIds = mov_Data.Actors_Movies.Select(a => a.ActorId).ToList()
                };
                // Using service it return to the form with dropdown data in the form taking Multiple data different table using ViewModels/NewMovieDropdownVM

                var formDropdownData = await _service.GetNewMovieDropdownValuesAsync();

                ViewBag.Cinemas = new SelectList(formDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(formDropdownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(formDropdownData.Actors, "Id", "FullName");
                return View(oldMovieData);           
        }
        //POST:
        [HttpPost]
        public async Task<IActionResult> Edit(int id , NewMovieVM editMovie)
        {
            if (id != editMovie.Id) return View("NotFound");

            if(!ModelState.IsValid)
             {
            //Going to Create form For Movie with Dropdown data in it
                var formDropdownData = await _service.GetNewMovieDropdownValuesAsync();

                ViewBag.Cinemas = new SelectList(formDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(formDropdownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(formDropdownData.Actors, "Id", "FullName");
                return View(editMovie);
              }
            //through service updating
           await _service.UpdateMovieAsync(id ,editMovie);
               
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            //Getting Data of Cinema and movie from repository
            var allMovies =await _service.GetAllAsync(c => c.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {//search if it match with given string then return to index page of movie with the particular movie data
            //change both data which is searchString and MovieName(db) and Description(db) to lower case then compare
                var movieFilterResult = allMovies.Where(m => m.MovieName.ToLower().Contains(searchString.ToLower()) 
                                                 || m.Description.ToLower().Contains(searchString.ToLower())).ToList();

                /*//or you can also use
                var movieFilterResult = allMovies.Where(m => String.Equals(m.MovieName,searchString,StringComparison.CurrentCultureIgnoreCase)
                                                                 || String.Equals(m.Description,searchString,StringComparison.CurrentCultureIgnoreCase)).ToList();*/

                return View("Index",movieFilterResult);
            }
            //otherwise goto index page of movie with all movie showing
            return View("Index",allMovies);
        }
    }
}
