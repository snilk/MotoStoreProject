using System.Web.Mvc;
using MotoStore.Domain.DataManipulations;

namespace MotoStore.WebUI.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        [HttpGet]
        public JsonResult ShopInformation()
        {
            return Json(ShopInformationOperations.GetShopInformation(),JsonRequestBehavior.AllowGet);
        }
     
    }
}