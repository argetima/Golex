using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pizza.Models;

namespace Pizza.Controllers
{
    public class PizzaOnlineController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(int branch = 0)
        {
            ViewBag.branch = branch;
            return View();
        }

        public JsonResult GetOrderDetails(int id)
        {
            List<OrderDetails> model = new List<OrderDetails>();

            var details = db.orderDetails.Include("order").Include("item").Where(od => od.order.id == id).ToList();
            details.ForEach(o => model.Add(new OrderDetails 
            { 
             id = o.id,
             item = o.item,
             price = o.price,
             quantity = o.quantity
            }));

            var firstItem = details.FirstOrDefault();

            return Json(new { success = true, data = model, datetime = firstItem.order.datetime, total = firstItem.order.Total.ToString("0.00") }, JsonRequestBehavior.AllowGet);
        }

	}
}