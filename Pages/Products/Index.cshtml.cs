using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelAsp1.Data;
using ModelAsp1.Extensions;
using ModelAsp1.Models;

namespace ModelAsp1.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ModelAsp1.Data.ModelAsp1Context _context;

        public IndexModel(ModelAsp1.Data.ModelAsp1Context context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ProductGenre { get; set; }

        public async Task OnGetAsync()
        {
            // Linq pour avoir les genres
            IQueryable<string> genreQuery = from m in _context.Product
                                            orderby m.Genre
                                            select m.Genre;
            var products = from m in _context.Product
                         select m;
            //pour la recherche par nom
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Title.Contains(SearchString));
            }

            ///pour select par genre : filtre 
            if (!string.IsNullOrEmpty(ProductGenre))
            {
                products = products.Where(x => x.Genre == ProductGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Product = await products.ToListAsync();
        }


        public async Task<IActionResult> OnPostAddToCartAsync(int id, int quantity)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)//si produit n'existe pas
            {
                return NotFound();
            }

           Cart cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            cart.AddItem(product, quantity);
            HttpContext.Session.Set("Cart", cart);

            return RedirectToPage("Index");
        }
    }
}
