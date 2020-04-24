﻿using System.Web.Mvc;
using BookStore.Domain.DataManipulations;
using BookStore.Domain.ViewModels;

namespace BookStore.WebUI.Controllers
{
    [RoutePrefix("Order")]
    public class OrderController : Controller
    {
        // GET: Order
        [HttpPost]
        [Route("OrderInterCompose")]
        public JsonResult OrderInterCompose (string token)
        {
            var orderInfo = OrderOperations.GetOrderForComposeByToken(token);

            if (orderInfo == null)
            {
                return Json(new TokenVm
                {
                    IsCorrectToken = false
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(orderInfo,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("OrderCompose")]
        public JsonResult OrderCompose(OrderInfoVm order)
        {
            if (order.BookId == 0 || order.ShopId == 0 || order.Token == null)
            {
                return Json(new SuccessVm(false), JsonRequestBehavior.AllowGet);
            }

            return Json(OrderOperations.AddNewOrder(order), JsonRequestBehavior.AllowGet);
        }
    }
}