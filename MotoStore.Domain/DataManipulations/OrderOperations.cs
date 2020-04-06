using System;
using System.Collections.Generic;
using System.Linq;
using MotoStore.Domain.EF;
using MotoStore.Domain.ViewModels;

namespace MotoStore.Domain.DataManipulations
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
            var user = UsersOperations.GetUserByToken(orderInfoVm.Token);

            if (user == null)
            {
                return new SuccessVm(false);
            }

            using (var context = new MotoStoreContext())
            {
                var userWithinContext = context.Users.FirstOrDefault(us => us.Id == user.Id);

                if (userWithinContext == null)
                {
                    return new SuccessVm(false);
                }

                var motorcycle = context.Motorcycles.FirstOrDefault(moto => moto.Id == orderInfoVm.MotoId);
                var shopInformation = context.ShopInformations.FirstOrDefault(shop => shop.Id == orderInfoVm.ShopId);

                if (motorcycle == null || shopInformation == null)
                {
                    return new SuccessVm(false);
                }

                var order = new Order
                {
                    Address = orderInfoVm.Address,
                    User = userWithinContext,
                    ShopInformation = shopInformation,
                    Motorcycle = motorcycle,
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