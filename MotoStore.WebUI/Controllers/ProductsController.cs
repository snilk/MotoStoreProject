using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using MotoStore.Domain;

namespace MotoStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        [System.Web.Http.HttpGet]
        public JsonResult Motorcycles(string makeForList,int id)
        {
            //throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            Array motos = null;
            if (id==0)
            {
                motos = MotoGroup.getMotosArrayByMake(makeForList);
            }
            else
            {
                motos = MotoGroup.getMotoById(id);
            }
            if (motos.Length == 0) throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            return Json(motos, JsonRequestBehavior.AllowGet);

        }
        [System.Web.Mvc.HttpGet]
        public JsonResult GetUniqCategories()
        {
            Array categories = MotoGroup.getUniqCategories();
            return Json(categories,JsonRequestBehavior.AllowGet);
        }
    }
}