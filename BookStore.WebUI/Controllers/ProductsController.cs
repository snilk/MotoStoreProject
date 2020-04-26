using System.Web.Mvc;
using BookStore.Domain.DataManipulations;

namespace BookStore.WebUI.Controllers
{
    [RoutePrefix("Products")]
    public class ProductsController : Controller
    {
        // GET: Products
        [HttpGet]
        [Route("Books/{id:int}")]
        public JsonResult Books(int id)
        {
            return Json(BookOperations.GetBookById(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Books/{section}")]
        public JsonResult Books(string section)
        {
            return Json(BookOperations.GetBooksBySection(section), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Route("GetUniqSections")]
        public JsonResult GetUniqSections()
        {
            return Json(BookOperations.GetUniqSections(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("GetUniqLevels")]
        public JsonResult GetUniqLevels()
        {
            return Json(BookOperations.GetUniqLevels(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("GetUniqAuthors")]
        public JsonResult GetUniqAuthors()
        {
            return Json(BookOperations.GetUniqAuthors(), JsonRequestBehavior.AllowGet);
        }
    }
}