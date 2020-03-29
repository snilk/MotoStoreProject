using System.Web.Mvc;
using MotoStore.Domain.DataManipulations;
using MotoStore.Domain.EF;
using MotoStore.Domain.ViewModels;

namespace MotoStore.WebUI.Controllers
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
        public JsonResult RemoveMoto(int id)
        {
            // return Json(id, JsonRequestBehavior.AllowGet);
            return Json(AdminOperations.RemoveMotoById(id));
        }

        [HttpPost]
        public JsonResult AddNewMoto(Motorcycle newMoto)
        {
            //return Json(newMoto, JsonRequestBehavior.AllowGet);
            return Json(AdminOperations.AddNewMoto(newMoto));
        }

        [HttpPost]
        public JsonResult AddNewShop(ShopInformationVm shopInformationVm)
        {
            return Json(AdminOperations.AddShop(shopInformationVm));
        }

        [HttpPost]
        public JsonResult RemoveShop(int id)
        {
            // return Json(id, JsonRequestBehavior.AllowGet);
            return Json(AdminOperations.RemoveShopById(id));
        }
    }
}