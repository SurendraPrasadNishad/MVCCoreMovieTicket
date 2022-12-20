using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMovieTicket.Data;
using OnlineMovieTicket.Data.Services.cinema;
using OnlineMovieTicket.Data.Static;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var cin_Data = await _service.GetAllAsync();
            return View(cin_Data);
        }
 //Get:Url  cinemas/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CinemaLogo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            else
            await _service.AddAsync(cinema) ;
               ModelState.Clear();
            return RedirectToAction(nameof(Index));
        }
       
        //Get:  Actors/Details/id
        [AllowAnonymous]
        public async Task<IActionResult>  Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
               if (cinemaDetails == null)
                return View("NotFound");
            else
                return View(cinemaDetails); 
        }
        //Get: Actors/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null)
                return View("NotFound");
            else
                return View(cinemaDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CinemaLogo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            else
            {
                await _service.UpdateAsync(id,cinema);
                return RedirectToAction(nameof(Index));
            }
        }
        //Get: Delete Confirmation
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null)
                return View("NotFound");
            else
                return View(cinemaDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) 
                return View("NotFound");

            else
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
