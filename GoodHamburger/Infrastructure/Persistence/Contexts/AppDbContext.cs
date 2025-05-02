using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Xml;

namespace GoodHamburger.Infrastructure.Persistence.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(b =>
            {
                b.Property(u => u.Id).ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<OrderItem>(b =>
            {
                b.Property(u => u.Id).ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "X Burger",
                    Price = 5,
                    Type = EnumProductType.Sandwich
                },
                new Product
                {
                    Id = 2,
                    Name = "X Egg",
                    Price = 4.5m,
                    Type = EnumProductType.Sandwich
                },
                new Product
                {
                    Id = 3,
                    Name = "X Bacon",
                    Price = 7,
                    Type = EnumProductType.Sandwich
                },
                new Product
                {
                    Id = 4,
                    Name = "Fries",
                    Price = 2,
                    Type = EnumProductType.ExtraFries
                },
               new Product
               {
                   Id = 5,
                   Name = "Soft drink",
                   Price = 2.5m,
                   Type = EnumProductType.ExtraDrink
               }
            );
            


        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Order { get; set; } = default!;
    }
}
