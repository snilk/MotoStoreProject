using MotoStore.Domain.InterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStore.Domain.DataManipulations
{
    public class OrderOperations
    {
        public static Array GetOrderForComposeByToken(string token)
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            int? userId = UsersOperations.getUserIdByToken(token);
            if (userId!=null)
            {
                var shops = from s in context.Shop_information
                            select new {s.Id, s.adress, s.phone_1, s.phone_2 };
                var orderInfo = (from o in context.Users
                                where o.Id == userId
                                select new
                                {
                                    o.name,o.surname,o.phone,o.email,
                                    shops
                                }).ToArray();
              
                return orderInfo;
            }
            else
            {
                return null;
            }
        }
        public static bool addNewOrder(OrderInfo orderInfo)
        {
            int? id = UsersOperations.getUserIdByToken(orderInfo.token);
            if (id == null) return false;            
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            if (((context.Motorcycles.Where(x => x.Id == orderInfo.idMoto)).FirstOrDefault()) == null|| 
                ((context.Shop_information.Where(x => x.Id == orderInfo.idShop)).FirstOrDefault()) == null||
                ((context.Users.Where(x => x.Id ==(int)id)).FirstOrDefault()) == null) return false;
            Order order = new Order
            {
                id_moto=orderInfo.idMoto,id_shop=orderInfo.idShop,id_user=(int)id,adress=orderInfo.adress,status=false
            };
           context.Orders.Add(order);
            context.SaveChanges();
            return true;
        }

    }
}
