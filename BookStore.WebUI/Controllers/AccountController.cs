using System;
using System.Web.Mvc;
using BookStore.Domain.DataManipulations;
using BookStore.Domain.ViewModels;

namespace BookStore.WebUI.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        // GET: Account
        [HttpPost]
        [Route("Registration")]
        public JsonResult Registration(UserVm newUser)
        {
            newUser.RegistrationDate = DateTime.Now;
            newUser.IsAdmin = false;
            return Json(UsersOperations.AddNewUser(newUser), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Authorization")]
        public JsonResult Authorization(UserForAuthorization existUser)
        {
            return Json(UsersOperations.GetUserToken(existUser), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("IsCorrectTokenUser")]
        public JsonResult IsCorrectTokenUser(string token)
        {
            return Json(UsersOperations.CheckUserByToken(token), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("EnterAccount")]
        public JsonResult EnterAccount(string token)
        {
            return Json(UsersOperations.GetAccountInformation(token), JsonRequestBehavior.AllowGet);
        }
    }
}