using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMovieTicket.Data;
using OnlineMovieTicket.Data.Services.actor;
using OnlineMovieTicket.Data.Services.producer;
using OnlineMovieTicket.Data.Static;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Controllers
{
    [Authorize(Roles =UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        //Get: Producers/Index
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var Prod_Data =await _service.GetAllAsync();
            return View(Prod_Data);
        }
        //Get:Url  Producers/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureUrl,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            else
            await _service.AddAsync(producer) ;
               ModelState.Clear();
            return RedirectToAction(nameof(Index));
        }
       
        //Get:  Actors/Details/id
        [AllowAnonymous]
        public async Task<IActionResult>  Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
               if (producerDetails == null)
                return View("NotFound");
            else
                return View(producerDetails); 
        }
        //Get: Actors/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
                return View("NotFound");
            else
                return View(producerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureUrl,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            else
            {
                await _service.UpdateAsync(id,producer);
                return RedirectToAction(nameof(Index));
            }
        }
        //Get: Delete Confirmation
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
                return View("NotFound");
            else
                return View(producerDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) 
                return View("NotFound");

            else
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
