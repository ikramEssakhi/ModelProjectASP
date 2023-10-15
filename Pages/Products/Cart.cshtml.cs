using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelAsp1.Extensions;
using ModelAsp1.Models;

namespace ModelAsp1.Pages.Products
{
    public class CartModel : PageModel
    {
        public List<CartLine> CartItems { get; set; }
        public void OnGet()
        {
            Cart cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            CartItems = cart.Items;
        }
    }
}
