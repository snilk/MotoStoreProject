using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStore.Domain.DataManipulations
{
    public  class AdminOperations
    {
        public static object GetInfoFormAdmin()
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            var orders = from o in context.Orders
                          join u in context.Users on o.id_user equals u.Id
                          join m in context.Motorcycles on o.id_moto equals m.Id
                          join s in context.Shop_information on o.id_shop equals s.Id
                          select new
                          {
                              orderId = o.Id,
                              homeAdress = o.adress,
                              o.date_compose,
                              o.status,
                              userId = u.Id,
                              u.name,
                              u.surname,
                              u.phone,
                              u.email,
                              motoId = m.Id,
                              shopAdress = s.adress,
                              s.phone_1
                          } ;

            var motos = from m in context.Motorcycles
                             select new
                             {
                                 m.Id,
                                 m.make,
                                 m.isElectrostarter,
                                 m.isCruizeControl,
                                 m.isABS,
                                 m.number_of_cilindrs,
                                 m.number_of_models,
                                 m.price,
                                 m.type,
                                 m.year_of_issue,
                                 m.description,
                                 m.engine_capacity,
                                
                             } ;
            var n = new { orders, motos };
            return n;
        }
        public static bool ChangeOrderStatus(int id)
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            var changedOrder = context.Orders.Where(o => o.Id == id).FirstOrDefault();
            if (changedOrder == null) return false;
            //changedOrder.status = changedOrder.status ? false : true;
            if (changedOrder.status)
            {
                changedOrder.status = false;
                var degMoto =
               context.Motorcycles.Where(m => m.Id == changedOrder.id_moto).FirstOrDefault();
                degMoto.number_of_models = degMoto.number_of_models == 0 ? 0 : --degMoto.number_of_models;
            }
            else
            {
                changedOrder.status = true;
              context.Motorcycles.Where(m => m.Id == changedOrder.id_moto).FirstOrDefault().number_of_models += 1;
            }
            context.SaveChanges();
            return true;       
        }
        public static bool RemoveMotoById(int id)
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            var removedMoto = context.Motorcycles.Where(m => m.Id == id).FirstOrDefault();
            if (removedMoto == null) return false;
            context.Motorcycles.Remove(removedMoto);
            context.SaveChanges();
            return true;
        }
        public static bool AddNewMoto(Motorcycle moto)
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            moto.main_photo = null;
            context.Motorcycles.Add(moto);
            context.SaveChanges();
            var motoFromDB=context.Motorcycles.Where(m => m.main_photo == null).FirstOrDefault();
            context.Moto_photos.AddRange(new List<Moto_photos>()
            {
                new Moto_photos(){id_moto=motoFromDB.Id,photo_url="1051"},
                new Moto_photos(){id_moto=motoFromDB.Id,photo_url="1052"},
                new Moto_photos(){id_moto=motoFromDB.Id,photo_url="1053"},
            });
            motoFromDB.main_photo = "1051";
            context.SaveChanges();
            return true;
        }
    }
}
