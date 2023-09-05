using About;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DataStore.SQL
{
    public class Travel  : IdentityDbContext<IdentityUser>
    {
        public Travel(DbContextOptions  options) : base (options)
        {

        }

        public DbSet<Country> Country { get; set; }
        public DbSet<City> Citiy { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Image> Image { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
           

        //    modelBuilder.Entity<Country>()
        //        .HasData(
        //        new Country { CountryId = 1, CountryName = "O'zbekiston" },
        //        new Country { CountryId = 2, CountryName = "Fransiya" }
        //        );
        //    modelBuilder.Entity<City>().HasData(
        //        new City { CityId = 1, CityName = "Toshkent", CountryConnectId = 1 },
        //        new City { CityId = 2, CityName = "Paris", CountryConnectId = 2 }
        //        );
        //    modelBuilder.Entity<Hotel>().HasData(
        //         new Hotel
        //         {
        //             HotelId = 1,HotelName = "Hilton",HotelStar = 3,HotelPrice = 400,
        //             HotelDescription = "Located in Tashkent, Hilton Tashkent City has a bar and a terrace.",
        //             CityConnctId = 1
        //         },
        //        new Hotel
        //        {
        //            HotelId = 2,HotelName = "B&B",HotelStar = 5,HotelPrice = 500,
        //            HotelDescription = "Отель B&B HOTEL Paris 17 Batignolles расположен на северо-западе Парижа,",
        //            CityConnctId = 2
        //        }
        //        );

        //    modelBuilder.Entity<City>()
        //        .HasOne<Country>(a => a.Country)
        //        .WithMany(a => a.Cities)
        //        .HasForeignKey(a => a.CityId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<Country>()
        //        .HasMany<City>(a => a.Cities)
        //        .WithOne(a => a.Country)
        //        .HasForeignKey(a => a.CountryConnectId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<City>()
        //        .HasMany<Hotel>(a => a.Hotels)
        //        .WithOne(a => a.City)
        //        .HasForeignKey(a => a.CityConnctId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<Hotel>()
        //        .HasMany<Image>(a => a.Images)
        //        .WithOne(a => a.Hotel)
        //        .HasForeignKey(a => a.HotelConnctId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}

    }
}