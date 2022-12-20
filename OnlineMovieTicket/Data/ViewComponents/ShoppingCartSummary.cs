using Microsoft.AspNetCore.Mvc;
using OnlineMovieTicket.Data.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        //Injecting ShoppingCart.cs
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        //Tocall this ViewComponent we have name Invoke Method so  we  use IviewComponent 
        public IViewComponentResult Invoke()
        {
            //Getting List of items in ShoppingCart 
            var items = _shoppingCart.GetShoppingCartItems();
            //count no of items present in list of Shopping Cart and to display count of diffrent items  them near icon
            return  View(items.Count);
            
        }
    }
}
