using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();
            if (context.Users.Any())
            {
                return;
            }

            var hasher = new PasswordHasher<User>();

            var testUser = new User
            {
                UserLogin = "testadmin"
            };

            testUser.UserPassword = hasher.HashPassword(testUser, "Pa$$w0rd");

            context.Users.Add(testUser);
            context.SaveChanges();
        }
    }
}
