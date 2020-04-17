using System;
using System.Linq;
using BookStore.Domain.EF;

namespace BookStore.Domain.DataInitialize
{
    public static class DataInitializer
    {
        public static void InitializeTables()
        {
            using (var context = new BookStoreContext())
            {
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

        public static void RemoveDuplicates()
        {
            using (var context = new BookStoreContext())
            {
                var query = context.Users.OrderBy(u=>u.Id).GroupBy(x => new { x.UserName, x.Password, x.Name, x.Surname, x.Phone, x.RegistrationDate, x.Email, x.IsAdmin })
                    .SelectMany(x => x.OrderBy(y=>y.Id).Skip(1));
                if (query.Any())
                {
                    context.Users.RemoveRange(query);
                }
                context.SaveChanges();
            }
        }
    }
}