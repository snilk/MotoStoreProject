using System.Web.Mvc;
using BookStore.Domain.DataManipulations;
using BookStore.Domain.ViewModels.Survey;

namespace BookStore.WebUI.Controllers
{
    [RoutePrefix("Survey")]
    public class SurveyController : Controller
    {
        [HttpPost]
        [Route("Submit")]
        public JsonResult Submit(SubmitSurveyVm submitSurveyVm)
        {
            return Json(SurveyOperations.SubmitSurvey(submitSurveyVm), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("GetAverageRate")]
        public JsonResult GetAverageRate()
        {
            return Json(SurveyOperations.GetAverageRate(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("GetPopularBooksByGoal/{goal}")]
        public JsonResult GetPopularBooksByGoal(string goal)
        {
            return Json(SurveyOperations.GetPopularBooks(goal), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("GetPopularBooks/{department}/{goal}")]
        public JsonResult GetPopularBooks(string department, string goal)
        {
            return Json(SurveyOperations.GetPopularBooks(department, goal), JsonRequestBehavior.AllowGet);
        }
    }
}