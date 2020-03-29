using System;
using System.Linq;
using MotoStore.Domain.DataManipulations;
using MotoStore.Domain.EF;

namespace MotoStore.Domain
{
    public static class UsersOperations
    {
        public static bool? addNewUser(User newUser)
        {
            if (newUser == null) return null;
            var context = new MotoStoreContext();
            var isExist = (from u in context.Users where u.UserName == newUser.UserName select u.UserName)
                .FirstOrDefault();
            if (isExist != null)
                return false;
            context.Users.Add(newUser);
            context.SaveChanges();
            return true;
        }

        public static UserTokenRole getUserToken(UserForAuthorization user)
        {
            var context = new MotoStoreContext();
            var existUser = (from u in context.Users where u.UserName == user.UserName select u).FirstOrDefault();
            if (existUser == null)
                return new UserTokenRole {correctUsername = false, correctPassword = null, token = null, IsAdmin = null};
            if (existUser.Password == user.Password)
                return new UserTokenRole
                {
                    IsAdmin = existUser.IsAdmin, token = getTokenById(existUser), correctPassword = true,
                    correctUsername = true
                };
            return new UserTokenRole {correctPassword = false, correctUsername = true, token = null, IsAdmin = null};
        }

        public static string getTokenById(User user)
        {
            var keystart = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9eyJzdWIiOiIxMjM0NSIsIm5h";
            var keylast = "bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
            return keystart + user.Id + keylast;
        }

        public static object checkUserByToken(string token)
        {
            var keystart = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9eyJzdWIiOiIxMjM0NSIsIm5h";
            var keylast = "bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
            int? tokenId = null;
            try
            {
                tokenId = Convert.ToInt32(token.Replace(keystart, "").Replace(keylast, ""));
            }
            catch (Exception)
            {
                tokenId = null;
            }

            if (tokenId == null) return false;
            var context = new MotoStoreContext();
            var roleAndCorrect =
                (from u in context.Users where u.Id == tokenId select new {IsAdmin = u.IsAdmin, isCorrectToken = true})
                .FirstOrDefault();
            return roleAndCorrect;
        }

        public static int? getUserIdByToken(string token)
        {
            var keystart = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9eyJzdWIiOiIxMjM0NSIsIm5h";
            var keylast = "bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
            int? tokenId = null;
            try
            {
                tokenId = Convert.ToInt32(token.Replace(keystart, "").Replace(keylast, ""));
            }
            catch (Exception)
            {
                tokenId = null;
            }

            if (tokenId == null) return null;
            var context = new MotoStoreContext();
            if ((from u in context.Users where u.Id == tokenId select u).FirstOrDefault() != null) return tokenId;
            return null;
        }

        public static Array getAccountInformation(string token)
        {
            var id = getUserIdByToken(token);
            if (id == null) return null;
            var context = new MotoStoreContext();
            var orderInfo = from o in context.Orders
                join m in context.Motorcycles on o.MotoId equals m.Id
                join s in context.ShopInformations on o.ShopId equals s.Id
                join u in context.Users on o.UserId equals u.Id
                where u.Id == (int) id
                select new
                {
                    motoId = m.Id, Make = m.Make, Type = m.Type, Year = m.Year, Price = m.Price, MainImage = m.MainImage,
                    shopAdress = s.Address, Phone1 = s.Phone1, Phone2 = s.Phone2,
                    orderId = o.Id, homeAdress = o.Address, Status = o.Status, OrderDate = o.OrderDate
                };
            var userAndOrdersInfo = (from u in context.Users
                where u.Id == (int) id
                select new
                {
                    Name = u.Name,
                    Surname = u.Surname,
                    Phone = u.Phone,
                    Email = u.Email,
                    RegistrationDate = u.RegistrationDate,
                    orders = orderInfo
                }).ToArray();

            foreach (var item in userAndOrdersInfo)
            {
            }

            return userAndOrdersInfo;
        }
    }
}