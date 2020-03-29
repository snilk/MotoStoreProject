using System;
using System.Collections.Generic;
using System.Linq;
using MotoStore.Domain.EF;
using MotoStore.Domain.ViewModels;

namespace MotoStore.Domain.DataManipulations
{
    public class OrderOperations
    {
        public static Array GetOrderForComposeByToken(string token)
        {
            var context = new MotoStoreContext();
            var user = UsersOperations.GetUserByToken(token);
            if (user != null)
            {
                var shops = from s in context.ShopInformations
                    select new {s.Id, Address = s.Address, Phone1 = s.Phone1, Phone2 = s.Phone2};
                var orderInfo = (from o in context.Users
                    where o.Id == user.Id
                    select new
                    {
                        Name = o.Name, Surname = o.Surname, Phone = o.Phone, Email = o.Email,
                        shops
                    }).ToArray();

                return orderInfo;
            }

            return null;
        }

        public static bool addNewOrder(OrderInfo orderInfo)
        {
            var user = UsersOperations.GetUserByToken(orderInfo.token);
            if (user == null) return false;
            var context = new MotoStoreContext();
            if (context.Motorcycles.Where(x => x.Id == orderInfo.idMoto).FirstOrDefault() == null ||
                context.ShopInformations.Where(x => x.Id == orderInfo.idShop).FirstOrDefault() == null ||
                context.Users.Where(x => x.Id == user.Id).FirstOrDefault() == null) return false;
            var order = new Order
            {
                MotoId = orderInfo.idMoto, ShopId = orderInfo.idShop, UserId = user.Id, Address = orderInfo.Address,
                Status = false, OrderDate = DateTime.Now
            };
            int[] p = {1, 2, 3};

            context.Orders.Add(order);
            context.SaveChanges();
            return true;
        }

        public static List<OrderInfoAdminVm> GetAllOrders()
        {
            var context = new MotoStoreContext();
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
                motoId = order.Motorcycle.Id,
                shopAdress = order.ShopInformation.Address,
                Phone1 = order.ShopInformation.Phone1
            }).ToList();
        }
    }
}