using MotoStore.Domain;
using MotoStore.Domain.DataManipulations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotoStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
       
        public JsonResult EnterAdmin()
        {
            object a = AdminOperations.GetInfoFormAdmin();
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangeOrderStatus(int id)
        {   
            
            return Json(new { Success=AdminOperations.ChangeOrderStatus(id)},JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RemoveMoto(int id)
        {
           // return Json(id, JsonRequestBehavior.AllowGet);
            return Json(new { Success = AdminOperations.RemoveMotoById(id) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddNewMoto(Motorcycle newMoto)
        {
            //return Json(newMoto, JsonRequestBehavior.AllowGet);
            return Json(new { Success = AdminOperations.AddNewMoto(newMoto) }, JsonRequestBehavior.AllowGet);
        }
    }
}