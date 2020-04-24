﻿using System.Web.Mvc;
using BookStore.Domain.DataManipulations;
using BookStore.Domain.Static;
using BookStore.Domain.ViewModels;

namespace BookStore.WebUI.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        [HttpGet]
        [Route("EnterAdmin")]
        public JsonResult EnterAdmin()
        {
            return Json(AdminOperations.GetInfoFormAdmin(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("ChangeOrderStatus")]
        public JsonResult ChangeOrderStatus(int id)
        {
            return Json(AdminOperations.ChangeOrderStatus(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("RemoveBook")]
        public JsonResult RemoveBook(int id)
        {
            return Json(AdminOperations.RemoveBookById(id));
        }

        [HttpPost]
        [Route("AddNewBook")]
        public JsonResult AddNewBook(BookVm book)
        {
            return Json(AdminOperations.AddNewBook(book, Server.MapPath(BookImagesConstants.ImagesFolder)));
        }

        [HttpPost]
        [Route("AddNewShop")]
        public JsonResult AddNewShop(ShopInformationVm shopInformationVm)
        {
            return Json(AdminOperations.AddShop(shopInformationVm));
        }

        [HttpPost]
        [Route("RemoveShop")]
        public JsonResult RemoveShop(int id)
        {
            return Json(AdminOperations.RemoveShopById(id));
        }

        [HttpGet]
        [Route("api/Test1/{id:int}")]
        public JsonResult Test(int id)
        {
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("api/Test1/{name}")]
        public JsonResult Test1(string name)
        {
            return Json(name, JsonRequestBehavior.AllowGet);
        }
    }
}