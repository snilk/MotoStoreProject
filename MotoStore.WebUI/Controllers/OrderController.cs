using MotoStore.Domain.DataManipulations;
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
        public JsonResult OrderCompose(OrderInfoVm order)
        {
            if (order.MotoId == 0 || order.ShopId == 0 || order.Token == null)
            {
                return Json(new SuccessVm(false), JsonRequestBehavior.AllowGet);
            }

            return Json(OrderOperations.AddNewOrder(order), JsonRequestBehavior.AllowGet);
        }
    }
}