using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Bulky.WebClient.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Title = "HereThere and Everywhere",
                    Description = "Here, There and Everywhere is a celebration of Sudha Murty's literary journey and is her 200th title across genres and languages. Bringing together her best-loved stories from various collections alongside some new ones and a thoughtful introduction, here is a book that is, in every sense, as multifaceted as its author.",
                    Auther = "Sudha Murty",
                    ISBN = "1234567890123",
                    ListPrice = 200,
                    Price = 174,
                    Price50 = 160,
                    Price100 = 170,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Title = "THE ART OF HAPPINESS",
                    Description = "In this unique and important book, one of the world's great spiritual leaders offers his practical wisdom and advice on how we can overcome everyday human problems and achieve lasting happiness.",
                    Auther = "The Dalai Lama",
                    ISBN = "1234567890123",
                    ListPrice = 350,
                    Price = 320,
                    Price50 = 310,
                    Price100 = 300,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 3,
                    Title = "The Story of Tata: 1868 to 2021",
                    Description = "The first and only authorized biography on Tata Group including the Tata-Mistry legal battle, exclusive interviews with Ratan Tata, and never-before-seen photographs of the Tata family.",
                    Auther = "Peter Casey",
                    ISBN = "1234567890123",
                    ListPrice = 500,
                    Price = 450,
                    Price50 = 440,
                    Price100 = 430,
                    CategoryId = 1
                });
        }
    }
}
