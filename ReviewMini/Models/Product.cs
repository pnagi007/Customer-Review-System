using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewMini.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    public IList<Review> Reviews { get; set; } = new List<Review>();

    [NotMapped]
    public double AverageRating => Reviews.Any() ? Math.Round(Reviews.Average(r => r.Rating), 2) : 0.0;
    }
}
