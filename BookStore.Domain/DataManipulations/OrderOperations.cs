using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Domain.EF;
using BookStore.Domain.ViewModels;

namespace BookStore.Domain.DataManipulations
{
    public class OrderOperations
    {
        public static OrderComposeVm GetOrderForComposeByToken(string token)
        {
            var user = UsersOperations.GetUserByToken(token);

            if (user != null)
            {
                return new OrderComposeVm
                {
                    ShopsInformation = ShopInformationOperations.GetShopInformation(),
                    User = new OrderUserVm(user)
                };
            }

            return null;
        }

        public static SuccessVm AddNewOrder(OrderInfoVm orderInfoVm)
        {
            using (var context = new BookStoreContext())
            {
                var user = UsersOperations.GetUserByToken(orderInfoVm.Token, context);

                if (user == null)
                {
                    return new SuccessVm(false);
                }

                var book = context.Books.FirstOrDefault(book1 => book1.Id == orderInfoVm.BookId);
                var shopInformation = context.ShopInformations.FirstOrDefault(shop => shop.Id == orderInfoVm.ShopId);

                if (book == null || shopInformation == null)
                {
                    return new SuccessVm(false);
                }

                var order = new Order
                {
                    Address = orderInfoVm.Address,
                    User = user,
                    ShopInformation = shopInformation,
                    Book = book,
                    OrderDate = DateTime.Now,
                    Status = false
                };

                context.Orders.Add(order);
                context.SaveChanges();
            }

            return new SuccessVm(true);
        }

        public static List<OrderInfoAdminVm> GetAllOrders()
        {
            var context = new BookStoreContext();
            return context.Orders.Select(order => new OrderInfoAdminVm
            {
                orderId = order.Id,
                homeAdress = order.Address,
                OrderDate = order.OrderDate,
                Status = order.Status,
                userId = order.User.Id,
                Name = order.User.Name,
                Surname = order.User.Surname,
                Phone = order.User.Phone,
                Email = order.User.Email,
                BookId = order.Book.Id,
                shopAdress = order.ShopInformation.Address,
                Phone1 = order.ShopInformation.Phone1
            }).ToList();
        }
    }
}