using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModelAsp1.Data;
using ModelAsp1.Models;

namespace ModelAsp1.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ModelAsp1.Data.ModelAsp1Context _context;

        public IndexModel(ModelAsp1.Data.ModelAsp1Context context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.User != null)
            {
                User = await _context.User.ToListAsync();
            }
        }
    }
}
