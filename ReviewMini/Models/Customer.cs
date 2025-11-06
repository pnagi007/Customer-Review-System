using System.ComponentModel.DataAnnotations;

namespace ReviewMini.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
