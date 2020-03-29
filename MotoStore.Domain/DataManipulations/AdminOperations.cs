using System.Collections.Generic;
using System.Linq;
using MotoStore.Domain.EF;

namespace MotoStore.Domain.DataManipulations
{
    public class AdminOperations
    {
        public static object GetInfoFormAdmin()
        {
            var context = new MotoStoreContext();
            var orders = from o in context.Orders
                join u in context.Users on o.UserId equals u.Id
                join m in context.Motorcycles on o.MotoId equals m.Id
                join s in context.ShopInformations on o.ShopId equals s.Id
                select new
                {
                    orderId = o.Id,
                    homeAdress = o.Address,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    userId = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    Phone = u.Phone,
                    Email = u.Email,
                    motoId = m.Id,
                    shopAdress = s.Address,
                    Phone1 = s.Phone1
                };

            var motos = from m in context.Motorcycles
                select new
                {
                    m.Id,
                    Make = m.Make,
                    ElectricStarter = m.ElectricStarter,
                    CruizeControl = m.CruizeControl,
                    Abs = m.Abs,
                    Cylinders = m.Cylinders,
                    ModelsCount = m.ModelsCount,
                    Price = m.Price,
                    Type = m.Type,
                    Year = m.Year,
                    Description = m.Description,
                    EngineCapacity = m.EngineCapacity
                };
            var n = new {orders, motos};
            return n;
        }

        public static bool ChangeOrderStatus(int id)
        {
            var context = new MotoStoreContext();
            var changedOrder = context.Orders.Where(o => o.Id == id).FirstOrDefault();
            if (changedOrder == null) return false;
            //changedOrder.Status = changedOrder.Status ? false : true;
            if (changedOrder.Status)
            {
                changedOrder.Status = false;
                var degMoto =
                    context.Motorcycles.Where(m => m.Id == changedOrder.MotoId).FirstOrDefault();
                degMoto.ModelsCount = degMoto.ModelsCount == 0 ? 0 : --degMoto.ModelsCount;
            }
            else
            {
                changedOrder.Status = true;
                context.Motorcycles.Where(m => m.Id == changedOrder.MotoId).FirstOrDefault().ModelsCount += 1;
            }

            context.SaveChanges();
            return true;
        }

        public static bool RemoveMotoById(int id)
        {
            var context = new MotoStoreContext();
            var removedMoto = context.Motorcycles.Where(m => m.Id == id).FirstOrDefault();
            if (removedMoto == null) return false;
            context.Motorcycles.Remove(removedMoto);
            context.SaveChanges();
            return true;
        }

        public static bool AddNewMoto(Motorcycle moto)
        {
            var context = new MotoStoreContext();
            moto.MainImage = null;
            context.Motorcycles.Add(moto);
            context.SaveChanges();
            var motoFromDB = context.Motorcycles.Where(m => m.MainImage == null).FirstOrDefault();
            context.MotoImages.AddRange(new List<MotoImages>
            {
                new MotoImages {MotoId = motoFromDB.Id, ImageUrl = "1051"},
                new MotoImages {MotoId = motoFromDB.Id, ImageUrl = "1052"},
                new MotoImages {MotoId = motoFromDB.Id, ImageUrl = "1053"}
            });
            motoFromDB.MainImage = "1051";
            context.SaveChanges();
            return true;
        }
    }
}