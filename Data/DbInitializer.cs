using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
namespace Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Перевіряємо, чи є вже користувачі в базі
            if (context.Users.Any())
            {
                return; // База даних вже ініціалізована, перериваємо виконання
            }

            var hasher = new PasswordHasher<User>();

            var testUser = new User
            {
                UserLogin = "testadmin"
            };

            // Генеруємо хеш для відкритого пароля "Pa$$w0rd" та зберігаємо його
            testUser.UserPassword = hasher.HashPassword(testUser, "Pa$$w0rd");

            context.Users.Add(testUser);
            context.SaveChanges();
        }
    }
}
