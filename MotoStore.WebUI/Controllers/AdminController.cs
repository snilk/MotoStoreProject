using System.Web.Mvc;
using BookStore.Domain.DataManipulations;
using BookStore.Domain.Static;
using BookStore.Domain.ViewModels;

namespace BookStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        [HttpGet]
        public JsonResult EnterAdmin()
        {
            return Json(AdminOperations.GetInfoFormAdmin(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeOrderStatus(int id)
        {
            return Json(AdminOperations.ChangeOrderStatus(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveBook(int id)
        {
            return Json(AdminOperations.RemoveBookById(id));
        }

        [HttpPost]
        public JsonResult AddNewBook(BookVm book)
        {
            return Json(AdminOperations.AddNewBook(book, Server.MapPath(BookImagesConstants.ImagesFolder)));
        }

        [HttpPost]
        public JsonResult AddNewShop(ShopInformationVm shopInformationVm)
        {
            return Json(AdminOperations.AddShop(shopInformationVm));
        }

        [HttpPost]
        public JsonResult RemoveShop(int id)
        {
            return Json(AdminOperations.RemoveShopById(id));
        }
    }
}