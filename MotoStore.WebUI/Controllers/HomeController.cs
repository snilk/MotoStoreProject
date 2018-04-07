using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotoStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getUniqCategories()
        {
            var category = new[]
            {
                new{name="Yamaha"},
                new{name="Java"},
                new{name="Ig"}
            };
           return Json(category,JsonRequestBehavior.AllowGet);
        } 
    }
}