using System.Web.Mvc;
using BookStore.Domain.DataManipulations;

namespace BookStore.WebUI.Controllers
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