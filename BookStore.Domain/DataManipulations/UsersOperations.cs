using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Domain.EF;
using BookStore.Domain.ViewModels;

namespace BookStore.Domain.DataManipulations
{
    public static class UsersOperations
    {
        private const string KeyStart = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9eyJzdWIiOiIxMjM0NSIsIm5h";
        private const string KeyLast = "bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
        public static SuccessVm AddNewUser(UserVm newUser)
        {
            using (var context = new BookStoreContext())
            {
                if (newUser == null || context.Users.Any(u => u.UserName == newUser.UserName))
                {
                    return new SuccessVm(false);
                }

                context.Users.Add(CreateUser(newUser));
                context.SaveChanges();
            }

            return new SuccessVm(true);
        }

        public static UserTokenRole GetUserToken(UserForAuthorization user)
        {
            User existUser;
            using (var context = new BookStoreContext())
            {
                existUser = context.Users.FirstOrDefault(u => u.UserName == user.UserName);
            }

            if (existUser == null)
            {
                return new UserTokenRole
                    {CorrectUsername = false, CorrectPassword = null, Token = null, IsAdmin = null};
            }

            if (existUser.Password != user.Password)
            {
                return new UserTokenRole
                    {CorrectPassword = false, CorrectUsername = true, Token = null, IsAdmin = null};
            }

            return new UserTokenRole
            {
                IsAdmin = existUser.IsAdmin, Token = GetTokenById(existUser.Id), CorrectPassword = true,
                CorrectUsername = true
            };
            
        }

        public static TokenVm CheckUserByToken(string token)
        {
            var user = GetUserByToken(token);

            var tokenVm = new TokenVm
            {
                IsAdmin = false,
                IsCorrectToken = false
            };

            if (user != null)
            {
                tokenVm.IsAdmin = user.IsAdmin;
                tokenVm.IsCorrectToken = true;
            }

            return tokenVm;
        }

        public static User GetUserByToken(string token, BookStoreContext context)
        {
            User user = null;

            if (token!= null && token.Contains(KeyLast) && token.Contains(KeyStart))
            {
                var id = Convert.ToInt32(token.Replace(KeyStart, "").Replace(KeyLast, ""));

                user = context.Users.FirstOrDefault(u => u.Id == id);
            }

            return user;
        }

        public static User GetUserByToken(string token)
        {
            using (var context = new BookStoreContext())
            {
                User user = null;

                if (token!= null && token.Contains(KeyLast) && token.Contains(KeyStart))
                {
                    var id = Convert.ToInt32(token.Replace(KeyStart, "").Replace(KeyLast, ""));

                    user = context.Users.FirstOrDefault(u => u.Id == id);
                }

                return user;
            }
        }

        public static AccountInformationVm GetAccountInformation(string token)
        {
            using (var context = new BookStoreContext())
            {
                var user = GetUserByToken(token, context);

                if (user == null)
                {
                    return null;
                }

                var userVm = new OrderUserVm(user);
                var userOrders = user.Orders.ToList();

                var accountOrderInformationVmList = new List<AccountOrderInformationVm>();

                foreach (var userOrder in userOrders)
                {
                    var orderInformationVm = new OrderAccountInfoVm(userOrder);
                    var bookVm = new BookVm(userOrder.Book);
                    var shopInformationVm = new ShopInformationVm(userOrder.ShopInformation);

                    var accountOrderInformationVm = new AccountOrderInformationVm
                    {
                        ShopInformation = shopInformationVm,
                        Book = bookVm,
                        OrderInformation = orderInformationVm
                    };

                    accountOrderInformationVmList.Add(accountOrderInformationVm);
                }

                return new AccountInformationVm
                {
                    AccountOrdersInformation = accountOrderInformationVmList,
                    User = userVm
                };
            }
        }

        public static User CreateUser(UserVm userVm)
        {
            return new User
            {
                Email = userVm.Email,
                IsAdmin = userVm.IsAdmin,
                Name = userVm.Name,
                Password = userVm.Password,
                Phone = userVm.Phone,
                RegistrationDate = userVm.RegistrationDate,
                Surname = userVm.Surname,
                UserName = userVm.UserName
            };
        }

        private static string GetTokenById(int id)
        {
            return KeyStart + id + KeyLast;
        }
    }
}