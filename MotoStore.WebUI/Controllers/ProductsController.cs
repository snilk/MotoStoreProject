using System.Web.Mvc;
using BookStore.Domain.DataManipulations;

namespace BookStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        [HttpGet]
        public JsonResult Books(string section,int id)
        {
            return id == 0
                ? Json(BookOperations.GetBookBySection(section), JsonRequestBehavior.AllowGet)
                : Json(BookOperations.GetBookById(id), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetUniqSections()
        {
            return Json(BookOperations.GetUniqSections(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUniqLevels()
        {
            return Json(BookOperations.GetUniqLevels(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUniqAuthors()
        {
            return Json(BookOperations.GetUniqAuthors(), JsonRequestBehavior.AllowGet);
        }
    }
}