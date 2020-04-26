using System.Web.Mvc;
using BookStore.Domain.DataInitialize;
using BookStore.Domain.DataManipulations;
using BookStore.Domain.Static;
using BookStore.Domain.ViewModels;

namespace BookStore.WebUI.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        [HttpGet]
        [Route("EnterAdmin")]
        public JsonResult EnterAdmin()
        {
            return Json(AdminOperations.GetInfoFormAdmin(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("ChangeOrderStatus")]
        public JsonResult ChangeOrderStatus(int id)
        {
            return Json(AdminOperations.ChangeOrderStatus(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("RemoveBook")]
        public JsonResult RemoveBook(int id)
        {
            return Json(AdminOperations.RemoveBookById(id));
        }

        [HttpPost]
        [Route("AddNewBook")]
        public JsonResult AddNewBook(BookVm book)
        {
            return Json(AdminOperations.AddNewBook(book, Server.MapPath(BookImagesConstants.ImagesFolder)));
        }

        [HttpPost]
        [Route("AddNewShop")]
        public JsonResult AddNewShop(ShopInformationVm shopInformationVm)
        {
            return Json(AdminOperations.AddShop(shopInformationVm));
        }

        [HttpPost]
        [Route("RemoveShop")]
        public JsonResult RemoveShop(int id)
        {
            return Json(AdminOperations.RemoveShopById(id));
        }

        
        [HttpGet]
        [Route("GetAvailableSections")]
        public JsonResult GetAvailableSections()
        {
            return Json(DataInitializer.AvailableSections, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("GetAvailableLevels")]
        public JsonResult GetAvailableLevels()
        {
            return Json(DataInitializer.AvailableLevels, JsonRequestBehavior.AllowGet);
        }
    }
}