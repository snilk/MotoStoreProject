using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MotoStore.Domain;
using MotoStore.Domain.DataManipulations;
using MotoStore.Domain.EF;
using MotoStore.Domain.ViewModels;

namespace MotoStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpPost]
        public JsonResult Registration(UserVm newUser)
        {
            newUser.RegistrationDate = DateTime.Now;
            newUser.IsAdmin = false;
            return Json(UsersOperations.AddNewUser(newUser), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Authorization(UserForAuthorization existUser)
        {
            return Json(UsersOperations.GetUserToken(existUser), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsCorrectTokenUser(string token)
        {
            return Json(UsersOperations.CheckUserByToken(token), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EnterAccount(string token)
        {
            return Json(UsersOperations.GetAccountInformation(token), JsonRequestBehavior.AllowGet);
        }
    }
}