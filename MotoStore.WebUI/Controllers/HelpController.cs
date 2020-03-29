using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotoStore.Domain;
using MotoStore.Domain.EF;
using Newtonsoft.Json;

namespace MotoStore.WebUI.Controllers
{
    public class HelpController : Controller
    {
        string[] randType = { "Classic","Sports bike","Sport-tourist","Cruiser"};
        int[] randYear = { 2000, 2004, 2007, 2010, 2014, 2017, 2001, 1995, 1996 };
        double[] randCapacity = { 400, 450, 500, 550, 600, 650, 700, 800 };
        int[] randCilindrs = { 1,2,3,4,6,8};
        bool[] randAdditional = { true, false };
        double[] randPrice = { 500, 600, 700, 800, 1000, 2000 };
       // string randDescription = "Live to ride Ride to life";
        string[] randMake = { "Yamaha", "Jawa", "Izh", "BMW", "Harly-Davidson" };

        public JsonResult fillPhoto()
        {
            var context = new MotoStoreContext();
            foreach (var moto in context.Motorcycles)
            {
                for (int i = 0; i < 3; i++)
                {
                    context.MotoImages.Add(new MotoImage()
                    {
                        MotoId = moto.Id,
                        ImageUrl = string.Format("photomoto{0}", i)
                    });
                }

            }
            context.SaveChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult allMotoByMake()
        {
                      
                var context = new MotoStoreContext();
            foreach (string Make in randMake)
            {
                string filepath = @"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\json" + Make + ".json";
                var motoByMake = from m in context.Motorcycles                              
                               where m.Make == Make
                               select new
                               {
                                   m.Id,m.Make,m.Cylinders,m.Price,m.Type,m.Year,m.EngineCapacity,m.MainImage 
                               };
                System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(motoByMake));
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult oneMoto()
        {
            string filepath = @"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\json" + "singleMotoToArray" + ".json";
            var context = new MotoStoreContext();
            var photoForSingleMoto = from p in context.MotoImages
                                     where p.MotoId == 101
                                     select p.ImageUrl;
            var singleMoto = (from m in context.Motorcycles
                              where m.Id == 101
                              select new
                              {
                                  m.Id,
                                  m.Make,
                                  m.ElectricStarter,
                                  m.CruizeControl,
                                  m.Abs,
                                  m.Cylinders,
                                  m.ModelsCount,
                                  m.Price,
                                  m.Type,
                                  m.Year,
                                  m.Description,
                                  m.EngineCapacity,
                                  photos=photoForSingleMoto
                              }).ToArray();
            Array b = singleMoto;
            System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(b));
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updateMoto()
        {
            var context = new MotoStoreContext();
            foreach (Motorcycle moto in context.Motorcycles)
            {
                moto.MainImage = "mainp";
            }
            context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult allMotos()
        {
            var context = new MotoStoreContext();
            string filepath = @"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\json" + "AllMotos" + ".json";
            var allMoto = from m in context.Motorcycles                             
                             select new
                             {
                                 m.Id,
                                 m.Make,
                                 m.Cylinders,
                                 m.Price,
                                 m.Type,
                                 m.Year,
                                 m.EngineCapacity,
                                 m.MainImage
                             };
            System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(allMoto));
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updateHarley()
        {
            var context = new MotoStoreContext();
            foreach (Motorcycle moto in context.Motorcycles)
            {
                if (moto.Make == "Harly-Davidson") moto.Make = "Harley-Davidson";
            }
            context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updateTables()
        {
            var context = new MotoStoreContext();
            
            int i = 0;
            foreach (Motorcycle moto in context.Motorcycles)
            {
                i = 1;
                moto.MainImage = Convert.ToString(moto.Id) + "1";
                foreach (var photots in context.MotoImages.Where(p=>p.MotoId==moto.Id))
                {
                    photots.ImageUrl = Convert.ToString(moto.Id) + i;
                    i++;
                }
            }
            context.SaveChanges();
            return Json("Success",JsonRequestBehavior.AllowGet);
           
        }
        // GET: Help
        //public JsonResult fillMotoData()
        //{
        //    Random r = new Random();
        //    MotoStoreDBEntities context = new MotoStoreDBEntities();
        //    List<Motorcycle> range = new List<Motorcycle>();
        //    Response.Write("Begin writing in database");
        //    foreach (string rMake in randMake)
        //    {
        //        for (int i = 0; i < 5; i++)
        //        {
        //            range.Add(new Motorcycle
        //            {
        //                Make = rMake,
        //                Type = randType[r.Next(0, randType.Length)],
        //                Year = randYear[r.Next(0, randYear.Length)],
        //                EngineCapacity = randCapacity[r.Next(0, randCapacity.Length)],
        //                Cylinders = randCilindrs[r.Next(0, randCilindrs.Length)],
        //                Abs = randAdditional[r.Next(0, randAdditional.Length)],
        //                CruizeControl = randAdditional[r.Next(0, randAdditional.Length)],
        //                ElectricStarter = randAdditional[r.Next(0, randAdditional.Length)],
        //                ModelsCount = 3,
        //                Price = randPrice[r.Next(0, randPrice.Length)],
        //                Description = randDescription
        //            });
        //        }
        //    }
        //    context.Motorcycles.AddRange(range);
        //    context.SaveChanges();
        //    Response.Write("Ending Writing");
        //    return Json(range, JsonRequestBehavior.AllowGet);
        //}

    }
}