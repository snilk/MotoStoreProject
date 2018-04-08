using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotoStore.Domain;

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
        string randDescription = "Live to ride Ride to life";
        string[] randMake = { "Yamaha", "Jawa", "Izh", "BMW", "Harly-Davidson" };

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