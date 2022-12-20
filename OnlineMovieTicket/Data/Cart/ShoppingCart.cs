using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.Cart
{
    public class ShoppingCart
    {
        //Injectecting ApplicationDBContext Class for database operation
        public ApplicationDBContext _context;
        public ShoppingCart(ApplicationDBContext context)
        {
            _context = context;
        }
        /*You have created ShoppingCart and injected in OrderController but you have to configure ShoppingCart in Startup.cs,
        Throughout in this application when you going to use ShoppingCart you going to create session and you are going to use
        this session to buy order item or to additem just to the ShoppingCart
         */
        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            //Getting Session with help of ServiceProvider if null then HttpContext.session
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<ApplicationDBContext>();
            //checking if we have cartId in Session otherwise generate new CartId
            string CartId = session.GetString("CartId")??Guid.NewGuid().ToString();
            //putting CartId in session
            session.SetString("CartId", CartId);
            return new ShoppingCart(context) { ShoppingCartId = CartId };

        }
        //Defining shoppingCardId and List<ShoppingCartItem>
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
       
        //Get:ShoppingCartItems
        public List<ShoppingCartItem> GetShoppingCartItems()
        {//nullable value ??  value we may get ,
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems
                                                             .Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
        }
        //Getting:Shoppingcart total Price
        public double GetShoppingCartTotal()
        {
         var totalPrice=   _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
            return totalPrice;
        }
        //Adding Movie To ShoppingCartItem Data come here from OrdersController/AddToShoppingCart(int id)
        public void AddMovieToShoppingCartItem(Movie movie)
        {
            //check if movie is already in ShoppingCartItems
            var ShoppingCartItem= _context.ShoppingCartItems.FirstOrDefault(n => n.ShoppingCartId==ShoppingCartId && n.Movie.Id==movie.Id);
            if (ShoppingCartItem==null)
            {
                ShoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                   Amount= 1
                };
                _context.ShoppingCartItems.Add(ShoppingCartItem);
            }
            else
            {
                ShoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }
        //Removing Movie from ShoppingCartItems
        public void RemoveMovieFromShoppingCartItem(Movie movie)
        {
            var ShoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.ShoppingCartId == ShoppingCartId && n.Movie.Id == movie.Id);
            //check if movie is already in ShoppingCartItems
            if (ShoppingCartItem != null)
            {//Checking no of Movie Added is greater then 1
                if(ShoppingCartItem.Amount>1)
                {
                    ShoppingCartItem.Amount--;
                }
                else
                {
                    //when we have 1 movie in ShoppingCart then Remove ShoppingCartItem Data
                    _context.ShoppingCartItems.Remove(ShoppingCartItem);
                }
            }
            _context.SaveChanges();
        }
        //Clear ShoppingCart After Adding Data to Database Order
        public async Task ClearShoppingCartAsync()
        {
            var Items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(Items);
            await _context.SaveChangesAsync();
            //for making count =0 when shopping cart added to db
            ShoppingCartItems = new List<ShoppingCartItem>();
        }

    }
}
