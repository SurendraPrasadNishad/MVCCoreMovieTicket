using Microsoft.EntityFrameworkCore;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.Services.orders
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDBContext _context;
        public OrdersService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId ,string userRole)
        {
            //contains all order movies when admin user return this order also include User ie: ApplicationUser
            var orders =await _context.Orders.Include(p => p.OrderItems).ThenInclude(p => p.Movie).Include(p=>p.User).ToListAsync();
            if (userRole!="Admin")
            {//contains only those order of a particular user who have log in 
                orders = orders.Where(o => o.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task OrderStoreAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
            };
          await  _context.Orders.AddAsync(order);
            _context.SaveChanges();
            //saving data in OrderItems table
            //Now when we Saved order then we can get the new Order Id from the database Orders from above other from ShoppingcartItems
            foreach (var Item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = Item.Amount,
                    MovieId=Item.Movie.Id,
                    OrderId=order.Id,    //getting orderId
                    Price=Item.Movie.Price
                };
                 await   _context.OrderItems.AddAsync(orderItem);
            }
                 await _context.SaveChangesAsync();
            //Afer adding to database table Orders clear ShoppingCart

        }


    }
}
