using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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
        public string Password { get; set; }
    }
 
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            //var db = new MotoStoreDBEntities();
            //db.Database.CreateIfNotExists();
            //db.Moto_photos.Add(new Moto_photos() {ImageUrl = "TestUtrl"});
            //db.SaveChanges();
            return View();
        }
        //public JsonResult getUniqCategories()
        //{
        //    MotoStoreDBEntities context = new MotoStoreDBEntities();
        //    var categories = ((from c in context.Motorcycles
        //                      select c.Make).Distinct()).ToArray();
        //    //var categories = new[]
        //    //{
        //    //    new{Make="BMW"},
        //    //    new{Make="Harly-Davidson"},
        //    //    new{Make="Izh"},
        //    //    new{Make="Jawa"},
        //    //    new{Make="Yamaha",}
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
        //    //sameClass.RegistrationDate = DateTime.Now;            
        //    //context.Users.Add(sameClass);
        //    //context.SaveChanges();
        //    return Json("Success");
            
        //}
    }
}