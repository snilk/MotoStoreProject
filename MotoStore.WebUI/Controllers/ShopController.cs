using MotoStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotoStore.WebUI.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public JsonResult shopInformaiton()
        {            
            return Json(ShopInformation.getShopInformation(),JsonRequestBehavior.AllowGet);
        }
    }
}