using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReviewMini.Data;
using ReviewMini.Models;

namespace ReviewMini.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;

    public List<ReviewMini.Models.Product> Products { get; set; } = new List<ReviewMini.Models.Product>();

        public async Task OnGetAsync()
        {
            Products = await _db.Products.Include(p => p.Reviews).ToListAsync();
        }
    }
}
