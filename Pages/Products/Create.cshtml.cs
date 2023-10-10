using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelAsp1.Data;
using ModelAsp1.Models;

namespace ModelAsp1.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ModelAsp1.Data.ModelAsp1Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateModel(ModelAsp1.Data.ModelAsp1Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
           /* if (!ModelState.IsValid || _context.Product == null || Product == null)
            {
                return Page();
            }
*/

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = DateTime.Now.Ticks + Path.GetExtension(ImageFile.FileName);

                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, fileName);

                Directory.CreateDirectory(uploadsFolder);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                Product.imageUrl = "/images/" + fileName; // nouvelle adresse d'img
            }

                _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}