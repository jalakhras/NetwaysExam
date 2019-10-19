using Microsoft.EntityFrameworkCore;
using System;

namespace Netways.EntityFramworkCore.Migrations.Seed.User
{
    public static class UserDataSeed
    {
        public static void Fill(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Model.User>().HasData(new Model.User() { LoginName = "Netways@Admin", DisplayName = "Netways", Address = "Lebanon / Beirut", CountryId = 10, DateofBirth = Convert.ToDateTime("03/12/1994"), IsActive = true, Salary = 1000 });

        }

    }
}
