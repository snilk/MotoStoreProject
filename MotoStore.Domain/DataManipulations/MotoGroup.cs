using System;
using System.Collections.Generic;
using System.Linq;
using MotoStore.Domain.EF;

namespace MotoStore.Domain
{
    public static class MotoGroup
    {
        public static Array getMotosArrayByMake(string Make)
        {
            var context = new MotoStoreContext();
            if (Make.ToLower() == "all")
            {
                var allMotos = (from m in context.Motorcycles
                    select new
                    {
                        m.Id,
                        Make = m.Make,
                        Cylinders = m.Cylinders,
                        Price = m.Price,
                        Type = m.Type,
                        Year = m.Year,
                        EngineCapacity = m.EngineCapacity,
                        MainImage = m.MainImage,
                        Abs = m.Abs,
                        CruizeControl = m.CruizeControl,
                        ElectricStarter = m.ElectricStarter,
                        ModelsCount = m.ModelsCount
                    }).ToArray();
                return allMotos;
            }

            var motoByMake = (from m in context.Motorcycles
                where m.Make == Make
                select new
                {
                    m.Id,
                    Make = m.Make,
                    Cylinders = m.Cylinders,
                    Price = m.Price,
                    Type = m.Type,
                    Year = m.Year,
                    EngineCapacity = m.EngineCapacity,
                    MainImage = m.MainImage,
                    Abs = m.Abs,
                    CruizeControl = m.CruizeControl,
                    ElectricStarter = m.ElectricStarter,
                    ModelsCount = m.ModelsCount
                }).ToArray();

            return motoByMake;
        }

        public static Array getMotoById(int id)
        {
            var context = new MotoStoreContext();
            var photoForSingleMoto = from p in context.MotoImages
                where p.MotoId == id
                select p.ImageUrl;
            var singleMoto = (from m in context.Motorcycles
                where m.Id == id
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
                    EngineCapacity = m.EngineCapacity,
                    MainImage = m.MainImage,
                    photos = photoForSingleMoto
                }).ToArray();
            var a = new Dictionary<string, string>
            {
                {"s", "aa"}
            };
            return singleMoto;
        }

        public static Array getUniqCategories()
        {
            var context = new MotoStoreContext();

            var categories = (from c in context.Motorcycles
                select new
                {
                    Make = c.Make
                }).Distinct().ToArray();
            //var categories = new[]
            //{
            //    new{Make="BMW"},
            //    new{Make="Harly-Davidson"},
            //    new{Make="Izh"},
            //    new{Make="Jawa"},
            //    new{Make="Yamaha",}
            //};
            return categories;
        }

        //public static bool AddNewMotorcycle(Motorcycle newMoto)
        //{
        //    MotoStoreDBEntities context = new MotoStoreDBEntities();
        //    context.Motorcycles.Add(newMoto);
        //    context.SaveChanges();
        //    return true;
        //}
    }
}