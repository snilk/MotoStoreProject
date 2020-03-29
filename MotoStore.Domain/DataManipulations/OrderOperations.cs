using System;
using System.Linq;
using MotoStore.Domain.EF;
using MotoStore.Domain.InterData;

namespace MotoStore.Domain.DataManipulations
{
    public class OrderOperations
    {
        public static Array GetOrderForComposeByToken(string token)
        {
            var context = new MotoStoreContext();
            var userId = UsersOperations.getUserIdByToken(token);
            if (userId != null)
            {
                var shops = from s in context.ShopInformations
                    select new {s.Id, Address = s.Address, Phone1 = s.Phone1, Phone2 = s.Phone2};
                var orderInfo = (from o in context.Users
                    where o.Id == userId
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
            var id = UsersOperations.getUserIdByToken(orderInfo.token);
            if (id == null) return false;
            var context = new MotoStoreContext();
            if (context.Motorcycles.Where(x => x.Id == orderInfo.idMoto).FirstOrDefault() == null ||
                context.ShopInformations.Where(x => x.Id == orderInfo.idShop).FirstOrDefault() == null ||
                context.Users.Where(x => x.Id == (int) id).FirstOrDefault() == null) return false;
            var order = new Order
            {
                MotoId = orderInfo.idMoto, ShopId = orderInfo.idShop, UserId = (int) id, Address = orderInfo.Address,
                Status = false, OrderDate = DateTime.Now
            };
            int[] p = {1, 2, 3};

            context.Orders.Add(order);
            context.SaveChanges();
            return true;
        }
    }
}