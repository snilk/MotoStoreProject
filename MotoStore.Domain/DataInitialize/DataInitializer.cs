using System;
using System.Data.Entity;
using System.Linq;
using MotoStore.Domain.EF;

namespace MotoStore.Domain.DataInitialize
{
    public static class DataInitializer
    {
        public static void InitializeTables()
        {
            var context = new MotoStoreContext();
            context.Database.Initialize(false);

            var admin = new User
            {
                Email = "admin@admin.com",
                Name = "Administrator",
                Password = "admin",
                Phone = "admin_phone",
                IsAdmin = true,
                Surname = "Administrator",
                UserName = "admin",
                RegistrationDate = DateTime.Now
            };

            if (!context.Users.Any(user => string.Equals(user.UserName, admin.UserName)))
            {
                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}