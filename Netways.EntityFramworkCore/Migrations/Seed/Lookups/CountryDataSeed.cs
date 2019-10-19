using Microsoft.EntityFrameworkCore;
using Netways.EntityFramworkCore.Model;

namespace Netways.EntityFramworkCore.Migrations.Seed.Lookups
{
    public static class CountryDataSeed
    {
        public static void Fill(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 1, Name = "Afghanistan" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 2, Name = "Åland" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 3, Name = "Albania" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 4, Name = "Algeria" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 5, Name = "American Samoa" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 6, Name = "Andorra" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 7, Name = "Anguilla" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 8, Name = "Antarctica" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 9, Name = "Antigua and Barbuda" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 10, Name = "Argentina" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 11, Name = "Armenia" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 12, Name = "Aruba" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 13, Name = "Australia" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 15, Name = "Azerbaijan" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 16, Name = "Bahamas" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 17, Name = "Bahrain" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 18, Name = "Bangladesh" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 19, Name = "Barbados" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 20, Name = "Belarus" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 21, Name = "Belgium" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 22, Name = "Belize" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 23, Name = "Benin" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 24, Name = "Bermuda" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 25, Name = "Bhutan" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 26, Name = "Bolivia" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 27, Name = "Bonaire" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 28, Name = "Bosnia and Herzegovina" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 29, Name = "Botswana" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 30, Name = "Bouvet Island" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 31, Name = "Brazil" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 32, Name = "British Indian Ocean Territory" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 33, Name = "British Virgin Islands" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 34, Name = "Brunei" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 35, Name = "Bulgaria" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 36, Name = "Burkina" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 37, Name = "Burundi" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 38, Name = "Cambodia" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 39, Name = "Cameroon" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 40, Name = "Canada" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 41, Name = "Cape Verde" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 42, Name = "Cayman Islands" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 43, Name = "Central African Republic" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 44, Name = "Chad" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 45, Name = "Cyprus" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 46, Name = "Chile" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 47, Name = "China" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 48, Name = "Christmas Island" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 49, Name = "Colombia" });
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 50, Name = "Comoros" });
        }

    }

}
