using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMovieTicket.Data;
using OnlineMovieTicket.Data.Services.actor;
using OnlineMovieTicket.Data.Static;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        //Get: Actors/Index
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var Act_Data =await _service.GetAllAsync();
            return View(Act_Data);
        }
        //Get:Url  Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureUrl,FullName,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            else
            await _service.AddAsync(actor) ;
               ModelState.Clear();
            return RedirectToAction(nameof(Index));
        }
        //Get:  Actors/Details/id
        [AllowAnonymous]
        public async Task<IActionResult>  Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
               if (actorDetails == null)
                return View("NotFound");
            else
                return View(actorDetails); 
        }
        //Get: Actors/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
                return View("NotFound");
            else
                return View(actorDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureUrl,FullName,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            else
            {
                await _service.UpdateAsync(id,actor);
                return RedirectToAction(nameof(Index));
            }
        }
        //Get: Delete Confirmation
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
                return View("NotFound");
            else
                return View(actorDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) 
                return View("NotFound");

            else
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
