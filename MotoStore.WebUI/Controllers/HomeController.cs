using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotoStore.Domain;
using System.Data.Entity.Validation;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MotoStore.WebUI.Controllers
{
    public class SameClass
    {
        public int Id { get; set; }
        public string password { get; set; }
    }
 
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }
        //public JsonResult getUniqCategories()
        //{
        //    MotoStoreDBEntities context = new MotoStoreDBEntities();
        //    var categories = ((from c in context.Motorcycles
        //                      select c.make).Distinct()).ToArray();
        //    //var categories = new[]
        //    //{
        //    //    new{make="BMW"},
        //    //    new{make="Harly-Davidson"},
        //    //    new{make="Izh"},
        //    //    new{make="Jawa"},
        //    //    new{make="Yamaha",}
        //    //};
        //    //string json = JsonConvert.SerializeObject(categories);
        //    //string json1 = JsonConvert.SerializeObject(JToken.FromObject("1"));
        //    //  System.IO.File.WriteAllText(@"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\jsonCategories.json", json);
        //    //  System.IO.File.WriteAllText(@"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\testToken.txt", json1);
        //    return categories;
        //}

     
        
        //[HttpPost]
        //public JsonResult addNewUser(User sameClass)
        //{
        //    //MotoStoreDBEntities context = new MotoStoreDBEntities();
        //    //sameClass.date_registration = DateTime.Now;            
        //    //context.Users.Add(sameClass);
        //    //context.SaveChanges();
        //    return Json("Success");
            
        //}
    }
}