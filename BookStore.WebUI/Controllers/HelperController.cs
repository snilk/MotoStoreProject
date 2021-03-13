using System.Web.Mvc;
using BookStore.Domain.Extensions;
using BookStore.Domain.Models;
using BookStore.Domain.Static;

namespace BookStore.WebUI.Controllers
{
    [RoutePrefix("Helper")]
    public class HelperController : Controller
    {
        [HttpGet]
        [Route("GetAvailableDepartments")]
        public JsonResult GetAvailableDepartments()
        {
            return Json(EnumExtensions.GetValuesDescriptions<Department>(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("GetPurchaseGoals")]
        public JsonResult GetPurchaseGoals()
        {
            return Json(PurchaseGoals.Goals, JsonRequestBehavior.AllowGet);
        }
    }
}