using Microsoft.EntityFrameworkCore;
using Netways.EntityFramworkCore.Migrations.Seed.Lookups;
using Netways.EntityFramworkCore.Migrations.Seed.User;

namespace Netways.EntityFramworkCore.Migrations.Seed
{
    public static class SeedHelper
    {
        public static void Fill(ModelBuilder modelBuilder)
        {
            CountryDataSeed.Fill(modelBuilder);
            UserDataSeed.Fill(modelBuilder);
        }

    }
}
