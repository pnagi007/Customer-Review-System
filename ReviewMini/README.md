# ReviewMini (Minimal Reviews System)

This is a minimal ASP.NET Core Razor Pages application that demonstrates a simple product review system using an in-memory EF Core database.

Features:
- Products, Reviews, Customers tables (models)
- Leave reviews with a rating (1-5) and comment
- Display average rating per product
- View reviews per product and filter by rating

Prerequisites:
- .NET 7 SDK (or compatible .NET SDK) installed

Run:

```powershell
cd "d:\mini project\ReviewMini"
dotnet run
```

Open https://localhost:5001 or http://localhost:5000 as shown in the console and navigate the site.

Notes:
- Uses EF Core InMemory for simplicity; data will reset each run.
- For a production app use a persistent database (SQLite, SQL Server, etc.) and add authentication.
