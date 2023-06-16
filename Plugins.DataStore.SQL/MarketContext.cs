using CoreBusiness;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.DataStore.SQL 
{
	public class MarketContext : DbContext
	{
		public MarketContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasMany(c => c.Products)
				.WithOne(p => p.Categories)
				.HasForeignKey(p => p.CategoryId);

			modelBuilder.Entity<Category>().HasData(
					new Category { CategoryId = 1, Name = "Beverage", Description = "Beverage" },
					new Category { CategoryId = 2, Name = "Bakery", Description = "Bakery" },
					new Category { CategoryId = 3, Name = "Meat", Description = "Meat" }
				);
			modelBuilder.Entity<Product>().HasData(
					new Product { ProductId = 1, CategoryId = 1, Name = "Iced Tea", Quantity = 200, Price = 1.99 },
					new Product { ProductId = 2, CategoryId = 1, Name = "Canada Dry", Quantity = 200, Price = 1.99 },
					new Product { ProductId = 3, CategoryId = 2, Name = "Whole Wheat Bread", Quantity = 300, Price = 1.50 },
					new Product { ProductId = 4, CategoryId = 2, Name = "White Bread", Quantity = 300, Price = 1.50 },
					new Product { ProductId = 5, CategoryId = 3, Name = "Beef Steak", Quantity = 700, Price = 250.94 },
					new Product { ProductId = 6, CategoryId = 3, Name = "Sheep Steak", Quantity = 700, Price = 285.49 }
				);
		}
	}
}
