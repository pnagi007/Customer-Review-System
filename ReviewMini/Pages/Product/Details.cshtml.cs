using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReviewMini.Data;
using ReviewMini.Models;

namespace ReviewMini.Pages.ProductPage
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _db;
        public DetailsModel(AppDbContext db) => _db = db;

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? RatingFilter { get; set; }

    public ReviewMini.Models.Product? Product { get; set; }
    public List<ReviewMini.Models.Review> Reviews { get; set; } = new List<ReviewMini.Models.Review>();
    public List<ReviewMini.Models.Customer> Customers { get; set; } = new List<ReviewMini.Models.Customer>();

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == null) return RedirectToPage("/Index");
            Product = await _db.Products.Include(p => p.Reviews).ThenInclude(r => r.Customer).FirstOrDefaultAsync(p => p.Id == Id);
            if (Product == null) return NotFound();

            Reviews = Product.Reviews.OrderByDescending(r => r.CreatedAt).ToList();
            if (RatingFilter.HasValue)
            {
                Reviews = Reviews.Where(r => r.Rating == RatingFilter.Value).ToList();
            }

            Customers = await _db.Customers.ToListAsync();
            return Page();
        }

        public class ReviewInput
        {
            [System.ComponentModel.DataAnnotations.Required]
            public int ProductId { get; set; }

            [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please select a customer.")]
            public int CustomerId { get; set; }

            [System.ComponentModel.DataAnnotations.Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
            public int Rating { get; set; }

            public string? Comment { get; set; }
        }

        [BindProperty]
        public ReviewInput Input { get; set; } = new ReviewInput();

        public async Task<IActionResult> OnPostAddReviewAsync()
        {
            // Basic server-side validation
            if (Input.ProductId == 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid product.");
            }

            if (!ModelState.IsValid)
            {
                Id = Input.ProductId;
                await OnGetAsync();
                return Page();
            }

            var review = new ReviewMini.Models.Review
            {
                ProductId = Input.ProductId,
                CustomerId = Input.CustomerId,
                Rating = Input.Rating,
                Comment = Input.Comment,
                CreatedAt = DateTime.UtcNow
            };

            var customer = await _db.Customers.FindAsync(Input.CustomerId);
            var product = await _db.Products.FindAsync(Input.ProductId);

            try
            {
                _db.Reviews.Add(review);
                await _db.SaveChangesAsync();

                TempData["Message"] = $"Thank you {customer?.Name}! Your {Input.Rating}-star review for {product?.Name} has been submitted.";

                // Reload the updated reviews and show the page with success message
                Id = Input.ProductId;
                await OnGetAsync();
                return Page();
            }
            catch (Exception)
            {
                // Log exception server-side (if a logger existed) and show friendly error
                TempData["Error"] = "An error occurred while saving your review. Please try again.";
                Id = Input.ProductId;
                await OnGetAsync();
                return Page();
            }
        }
    }
}
