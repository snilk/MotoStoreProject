using MotoStore.Domain.DataManipulations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotoStore.Domain.ViewModels;

namespace MotoStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [HttpPost]
        public JsonResult OrderInterCompose (string token)
        {
            Array orderInfo = OrderOperations.GetOrderForComposeByToken(token);
            if (orderInfo==null)
            {
                return Json(new {isCorrectToken=false },JsonRequestBehavior.AllowGet);
            }
            //string filepath = @"D:\универ\6_сем\бибд курсовой\MotoStore\JsonFIles\json" + "responseAfterClickOnOrder" + ".json";
            //System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(orderInfo));
            return Json(orderInfo,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult OrderCompose(OrderInfo order)
        {
           // return Json(order, JsonRequestBehavior.AllowGet);
            if (order.idMoto==0||order.idShop==0||order.token==null) return Json(new { isCorrectOrders=false},JsonRequestBehavior.AllowGet);
            //return Json("False",JsonRequestBehavior.AllowGet);
          
            return Json(new { isCorrectOrder=OrderOperations.addNewOrder(order)},JsonRequestBehavior.AllowGet);
        }
    }
}