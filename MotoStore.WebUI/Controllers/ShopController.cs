using System.Web.Mvc;
using BookStore.Domain.DataManipulations;

namespace BookStore.WebUI.Controllers
{
    [RoutePrefix("Shop")]
    public class ShopController : Controller
    {
        // GET: Shop
        [HttpGet]
        [Route("ShopInformation")]
        public JsonResult ShopInformation()
        {
            return Json(ShopInformationOperations.GetShopInformation(),JsonRequestBehavior.AllowGet);
        }
     
    }
}