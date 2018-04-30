using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoStore.Domain
{
   public static  class MotoGroup
    {
        public static Array getMotosArrayByMake(string make)
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            if (make.ToLower() == "all")
            {
                var allMotos = (from m in context.Motorcycles
                                select new
                                {
                                    m.Id,
                                    m.make,
                                    m.number_of_cilindrs,
                                    m.price,
                                    m.type,
                                    m.year_of_issue,
                                    m.engine_capacity,
                                    m.main_photo
                                }).ToArray();
                return allMotos;
            }
            var motoByMake = (from m in context.Motorcycles
                             where m.make == make
                             select new
                             {
                                 m.Id,
                                 m.make,
                                 m.number_of_cilindrs,
                                 m.price,
                                 m.type,
                                 m.year_of_issue,
                                 m.engine_capacity,
                                 m.main_photo
                             }).ToArray();
            return motoByMake;
        }
        public static Array getMotoById(int id)
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            var photoForSingleMoto = from p in context.Moto_photos
                                     where p.id_moto == id
                                     select p.photo_url;
            var singleMoto = (from m in context.Motorcycles
                              where m.Id == id
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
                                  m.main_photo,
                                  photos = photoForSingleMoto
                              }).ToArray();
            
            return singleMoto;
        }
        public static Array getUniqCategories()
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
           
            var categories = ((from c in context.Motorcycles
                              select new
                              {
                                  c.make
                              }).Distinct()).ToArray();
            //var categories = new[]
            //{
            //    new{make="BMW"},
            //    new{make="Harly-Davidson"},
            //    new{make="Izh"},
            //    new{make="Jawa"},
            //    new{make="Yamaha",}
            //};
            return categories;
        }
    }
}
