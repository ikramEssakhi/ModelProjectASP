using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelAsp1.Data;
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
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Product
                                            orderby m.Genre
                                            select m.Genre;
            var products = from m in _context.Product
                         select m;
            //for the search by name
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Title.Contains(SearchString));
            }

            ///fot the select by name : filtre 
            if (!string.IsNullOrEmpty(ProductGenre))
            {
                products = products.Where(x => x.Genre == ProductGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Product = await products.ToListAsync();
        }
    }
}
