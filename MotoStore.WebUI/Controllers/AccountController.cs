using MotoStore.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotoStore.Domain.DataManipulations;
using Newtonsoft.Json;

namespace MotoStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        Dictionary<string, bool?> success = new Dictionary<string, bool?>();
        // GET: Account
        [HttpPost]
        public JsonResult Registration(User newUser)
        {
            newUser.date_registration = DateTime.Now;
            newUser.role = false;
            //string filepath = @"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\json" + "testNewUser" + ".json";
            //System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(newUser));
            success.Add("success", UsersOperations.addNewUser(newUser));           
            return Json(success,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
         public JsonResult Authorization(UserForAuthorization existUser)//ForAuthorization
        {
            //string filepath = @"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\json" + "testUserAutoriz" + ".json";           
            //var us = UsersOperations.getUserToken(existUser);
            //System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(existUser));
            return Json(UsersOperations.getUserToken(existUser), JsonRequestBehavior.AllowGet);//
        }
        [HttpPost]
        public JsonResult isCorrectTokenUser(string token)
        {
            //token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NSIsIm5h1006bWUiOiJKb2huIEdvbGQiLCJhZG1pbiI6dHJ1ZX0.LIHjWCBORSWMEibq - tnT8ue_deUqZx1K0XxCOXZRrBI";
            //string filepath = @"D:\универ\6 сем\бибд курсовой\MotoStore\JsonFIles\json" + "responseAfterCheckingToken" + ".json";
            //System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(new { isCorrectToken = UsersOperations.checkUserByToken(token) }));
            return Json(UsersOperations.checkUserByToken(token) ,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EnterAccount(string token)
        {
          //  string filepath = @"D:\универ\6_сем\бибд курсовой\MotoStore\JsonFIles\json" + "responseAfterEnterAccount" + ".json";
          //  System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(UsersOperations.getAccountInformation(token)));
          //  string filepath1 = @"D:\универ\6_сем\бибд курсовой\MotoStore\JsonFIles\json" + "responseAfterAddNewOrder" + ".json";
          //  bool s = false;
          //  System.IO.File.WriteAllText(filepath1, JsonConvert.SerializeObject(new { isCorrectOrder =s  }));
            return Json(UsersOperations.getAccountInformation(token), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult Testing()
        //{
        //    MotoStoreDBEntities context = new MotoStoreDBEntities();
           
                     
        //    return Json(ob, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult Authorization()
        //{
        //    string token=
        //    return (token)
        //}
    }
}