using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotoStore.Domain;
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

        //public JsonResult fillPhoto()
        //{
        //    MotoStoreDBEntities context = new MotoStoreDBEntities();
        //    foreach (Motorcycle moto in context.Motorcycles)
        //    {
        //        for (int i = 0; i < 3; i++)
        //        {
        //            context.Moto_photos.Add(new Moto_photos
        //            {
        //                id_moto = moto.Id,
        //                photo_url = string.Format("photomoto{0}",i)
        //            });
        //        }
                
        //    }
        //    context.SaveChanges();
        //    return Json("Success",JsonRequestBehavior.AllowGet);
        //}
        public JsonResult allMotoByMake()
        {
                      
                MotoStoreDBEntities context = new MotoStoreDBEntities();
            foreach (string make in randMake)
            {
                string filepath = @"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\json" + make + ".json";
                var motoByMake = from m in context.Motorcycles                              
                               where m.make == make
                               select new
                               {
                                   m.Id,m.make,m.number_of_cilindrs,m.price,m.type,m.year_of_issue,m.engine_capacity,m.main_photo 
                               };
                System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(motoByMake));
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult oneMoto()
        {
            string filepath = @"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\json" + "singleMotoToArray" + ".json";
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            var photoForSingleMoto = from p in context.Moto_photos
                                     where p.id_moto == 101
                                     select p.photo_url;
            var singleMoto = (from m in context.Motorcycles
                              where m.Id == 101
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
                                  photos=photoForSingleMoto
                              }).ToArray();
            Array b = singleMoto;
            System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(b));
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updateMoto()
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            foreach (Motorcycle moto in context.Motorcycles)
            {
                moto.main_photo = "mainp";
            }
            context.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult allMotos()
        {
            MotoStoreDBEntities context = new MotoStoreDBEntities();
            string filepath = @"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\json" + "AllMotos" + ".json";
            var allMoto = from m in context.Motorcycles                             
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
                             };
            System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(allMoto));
            return Json("success", JsonRequestBehavior.AllowGet);
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
        //                make = rMake,
        //                type = randType[r.Next(0, randType.Length)],
        //                year_of_issue = randYear[r.Next(0, randYear.Length)],
        //                engine_capacity = randCapacity[r.Next(0, randCapacity.Length)],
        //                number_of_cilindrs = randCilindrs[r.Next(0, randCilindrs.Length)],
        //                isABS = randAdditional[r.Next(0, randAdditional.Length)],
        //                isCruizeControl = randAdditional[r.Next(0, randAdditional.Length)],
        //                isElectrostarter = randAdditional[r.Next(0, randAdditional.Length)],
        //                number_of_models = 3,
        //                price = randPrice[r.Next(0, randPrice.Length)],
        //                description = randDescription
        //            });
        //        }
        //    }
        //    context.Motorcycles.AddRange(range);
        //    context.SaveChanges();
        //    Response.Write("Ending Writing");
        //    return Json(range,JsonRequestBehavior.AllowGet);
        //}
     
    }
}