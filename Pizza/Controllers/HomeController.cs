using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pizza.Models;
using Microsoft.AspNet.SignalR;
using Pizza.Hubs;

namespace Pizza.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            CategoryViewModelList model = new CategoryViewModelList();
            model.items = new List<Category>();
            model.items = db.category.Include("items").ToList();

            var cart = ShoppingCart.GetCart(this.HttpContext);
            model.countItems = cart.GetCount();
            return View(model);
        }

        public JsonResult SetDelay(int id)
        {
            Order order = db.orders
                .Include("OrderDetails")
                .Include("OrderDetails")
                .Include("PaymentMethod")
                .Where(o => o.id == id).FirstOrDefault();
            order.ordered = true;
            db.SaveChanges();

            Extensions.Extensions.order = order;
            Extensions.Extensions.PrintOrder();
            return Json(new { success = true });
        }

        public void updateHearingSessions()
        {
            var h = GlobalHost.ConnectionManager.GetHubContext<OrderHub>();

            List<OrderViewModel> sessions = db.orders.Where(o => o.ordered == false | o.ordered == null).OrderByDescending(o => o.id).AsEnumerable().Select(
                o => new OrderViewModel
                {
                    id = o.id,
                    phone = o.Phone,
                    adress = o.Address,
                    datetime = Extensions.Extensions.datetimeToString(o.datetime),
                    sum = o.Total.ToString("0.00")
                }).ToList();

            h.Clients.All.updateSessions(sessions);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}