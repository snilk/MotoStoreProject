using System.Web.Mvc;
using MotoStore.Domain.DataManipulations;

namespace MotoStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        [HttpGet]
        public JsonResult Motorcycles(string makeForList,int id)
        {
            return id == 0
                ? Json(MotorcycleOperations.GetMotorcyclesByMake(makeForList), JsonRequestBehavior.AllowGet)
                : Json(MotorcycleOperations.GetMotoById(id), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetUniqCategories()
        {
            return Json(MotorcycleOperations.GetUniqCategories(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUniqTypes()
        {
            return Json(MotorcycleOperations.GetUniqTypes(), JsonRequestBehavior.AllowGet);
        }
    }
}