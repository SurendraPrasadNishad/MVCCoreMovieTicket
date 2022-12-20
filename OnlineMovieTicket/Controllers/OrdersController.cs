using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieTicket.Data.Cart;
using OnlineMovieTicket.Data.Services.movie;
using OnlineMovieTicket.Data.Services.orders;
using OnlineMovieTicket.Data.Static;
using OnlineMovieTicket.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {//using dependency injection so we can acess MovieService and ShoppingCart in Data
        private readonly IMoviesService _movieService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _orderService;

        public OrdersController(IMoviesService movieService,ShoppingCart shoppingCart,IOrdersService orderService)
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;

        }
        //Showing all order that has completed by a particular user
        public async Task<IActionResult> Index()
        {//the person who log in get Id and Role through claim
            string userId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole =User.FindFirstValue(ClaimTypes.Role);
            var orders = await _orderService.GetOrdersByUserIdAndRoleAsync(userId,userRole);
            return View(orders);
        }
        //Get: List of item in ShoppingCart
        public IActionResult ShoppingCart()
        {//Running GetShoppingCarts() method present in _shoppingCart to fetch data from database
            var Items = _shoppingCart.GetShoppingCartItems();
            //putting Items in List of ShoppingCartItems  Present in _shoppingCart
                _shoppingCart.ShoppingCartItems=Items;
            //Adding thesedata to properties present in ShoppingCartVM
            var Response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(Response);
        }
        //When Add to cart button pressed then them movie id comes here and thisfunction run
        public async Task<RedirectToActionResult> AddToShoppingCart(int id)
        {//through movie Service getting particular movie Data
            var movData =await _movieService.GetMovieByIdAsync(id);
            if (movData != null)
            {//through ShoppingCart add particular movie Data to cart
                _shoppingCart.AddMovieToShoppingCartItem(movData);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        //When Remove From cart button pressed then movie id come here and thisfunction run

        public async Task<RedirectToActionResult> RemoveFromShoppingCart(int id)
        {//through movie Service getting particular movie Data
            var movData = await _movieService.GetMovieByIdAsync(id);
            if (movData != null)
            {//through ShoppingCart Remove particular movies Data from cart 
                _shoppingCart.RemoveMovieFromShoppingCartItem(movData);
            }
            return RedirectToAction(nameof(ShoppingCart));

        }
        //Forcompleting all order
        public async Task<IActionResult> OrderCompleted()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            //In User Identity there is id , email property get thatvaluepresent here When login
            string userId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress =User.FindFirstValue(ClaimTypes.Email);
            //storing data in db
            await _orderService.OrderStoreAsync(items, userId, userEmailAddress);
            //After adding to database clear the ShoppingCart from database
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
    }
}
