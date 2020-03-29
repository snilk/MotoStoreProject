using System;
using System.Linq;
using MotoStore.Domain.DataManipulations;
using MotoStore.Domain.EF;
using MotoStore.Domain.ViewModels;

namespace MotoStore.Domain
{
    public static class UsersOperations
    {
        private const string KeyStart = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9eyJzdWIiOiIxMjM0NSIsIm5h";
        private const string KeyLast = "bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
        public static bool? AddNewUser(User newUser)
        {
            var context = new MotoStoreContext();

            if (newUser == null || context.Users.Any(u => u.UserName == newUser.UserName))
            {
                return false;
            }
            context.Users.Add(newUser);
            context.SaveChanges();

            return true;
        }

        public static UserTokenRole GetUserToken(UserForAuthorization user)
        {
            var context = new MotoStoreContext();
            var existUser = context.Users.FirstOrDefault(u => u.UserName == user.UserName);
            if (existUser == null)
            {
                return new UserTokenRole {correctUsername = false, correctPassword = null, token = null, IsAdmin = null};
            }
            if (existUser.Password == user.Password)
                return new UserTokenRole
                {
                    IsAdmin = existUser.IsAdmin, token = getTokenById(existUser.Id), correctPassword = true,
                    correctUsername = true
                };
            return new UserTokenRole {correctPassword = false, correctUsername = true, token = null, IsAdmin = null};
        }

        public static string getTokenById(int id)
        {
            return KeyStart + id + KeyLast;
        }

        public static TokenVm CheckUserByToken(string token)
        {
            var user = GetUserByToken(token);

            var tokenVm = new TokenVm
            {
                IsAdmin = false,
                isCorrectToken = false
            };

            if (user != null)
            {
                tokenVm.IsAdmin = user.IsAdmin;
                tokenVm.isCorrectToken = true;
            }

            return tokenVm;
        }

        public static User GetUserByToken(string token)
        {
            User user = null;

            if (token.Contains(KeyLast) && token.Contains(KeyStart))
            {
                var id = Convert.ToInt32(token.Replace(KeyStart, "").Replace(KeyLast, ""));

                var context = new MotoStoreContext();
                user = context.Users.FirstOrDefault(u => u.Id == id);
            }

            return user;
        }

        public static Array getAccountInformation(string token)
        {
            var id = GetUserByToken(token);

            var user = GetUserByToken(token);


            if (user == null)
            {
                return null;
            }
            var context = new MotoStoreContext();

            var orderInfo = from o in context.Orders
                join m in context.Motorcycles on o.MotoId equals m.Id
                join s in context.ShopInformations on o.ShopId equals s.Id
                join u in context.Users on o.UserId equals u.Id
                where u.Id == user.Id
                select new
                {
                    motoId = m.Id, Make = m.Make, Type = m.Type, Year = m.Year, Price = m.Price, MainImage = m.MainImage,
                    shopAdress = s.Address, Phone1 = s.Phone1, Phone2 = s.Phone2,
                    orderId = o.Id, homeAdress = o.Address, Status = o.Status, OrderDate = o.OrderDate
                };
            var userAndOrdersInfo = (from u in context.Users
                where u.Id == user.Id
                select new
                {
                    Name = u.Name,
                    Surname = u.Surname,
                    Phone = u.Phone,
                    Email = u.Email,
                    RegistrationDate = u.RegistrationDate,
                    orders = orderInfo
                }).ToArray();

            return userAndOrdersInfo;
        }
    }
}