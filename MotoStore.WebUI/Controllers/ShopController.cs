using MotoStore.Domain;
using Newtonsoft.Json;
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
            //string filepath = @"D:\универ\6_сем\бибд курсовой\MotoStore\JsonFIles\json" + "shopInformation" + ".json";
              //System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(ShopInformation.getShopInformation()));
            return Json(ShopInformation.getShopInformation(),JsonRequestBehavior.AllowGet);
        }
     
    }
}