using ReviewMini.Models;

namespace ReviewMini.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Products.Any()) return;

            var customers = new[] {
                new Customer { Name = "Alice", Email = "alice@example.com" },
                new Customer { Name = "Bob", Email = "bob@example.com" }
            };
            db.Customers.AddRange(customers);
            
            var products = new[] {
                new Product { Name = "Blue T-Shirt", Description = "A comfortable blue t-shirt." },
                new Product { Name = "Coffee Mug", Description = "Ceramic mug, 350ml." },
                new Product { Name = "Notebook", Description = "A5 ruled notebook." }
            };
            db.Products.AddRange(products);
            db.SaveChanges();
            
            db.Reviews.Add(new Review { ProductId = products[0].Id, CustomerId = customers[0].Id, Rating = 4, Comment = "Nice fabric." });
            db.Reviews.Add(new Review { ProductId = products[0].Id, CustomerId = customers[1].Id, Rating = 5, Comment = "Fits well." });
            db.Reviews.Add(new Review { ProductId = products[1].Id, CustomerId = customers[0].Id, Rating = 3, Comment = "Good but small." });
            db.SaveChanges();
        }
    }
}
