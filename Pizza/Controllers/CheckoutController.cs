using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pizza.Models;
using Pizza.Hubs;
using Microsoft.AspNet.Identity;

namespace Pizza.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        DatabaseContext storeDB = new DatabaseContext();
        //ApplicationDbContext storeUsers = new ApplicationDbContext();
        const string PromoCode = "FREE";

        //
        // GET: /Checkout/AddressAndPayment

        public ActionResult AddressAndPayment()
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser user = storeDB.Users.Where(u => u.Id == userId).FirstOrDefault();
            Order o = new Order();
            o.Address = user.address;
            o.Phone = user.PhoneNumber;
            o.Email = user.Email;

            return View(o);
        }

        //
        // POST: /Checkout/AddressAndPayment

        [HttpPost]
        public ActionResult AddressAndPayment(Pizza.Models.Order model)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                //if (string.Equals(values["PromoCode"], PromoCode,
                //    StringComparison.OrdinalIgnoreCase) == false)
                //{
                //    return View(order);
                //}
                //else
                {
                    string username = User.Identity.Name;
                    var user = storeDB.Users.Where(u => u.UserName == username).FirstOrDefault();
                    //ShoppingCart ShoppingCart = new ShoppingCart();

                    order.username = user.UserName;
                    order.datetime = DateTime.Now;
                    order.Address = model.Address;
                    order.Phone = user.PhoneNumber;
                    order.Email = user.Email;

                    //order.Total = ShoppingCart.GetTotal();
                    //Save Order
                    storeDB.orders.Add(order);
                    storeDB.SaveChanges();

                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);
                    updateHearingSessions();
                    
                    return RedirectToAction("Complete", new { id = order.id });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete

        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.orders.Any(
                o => o.id == id &&
                o.username == User.Identity.Name);

            if (isValid)
            {
                string userId = User.Identity.GetUserId();
                var user = storeDB.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    Extensions.EmailService.SendAsync(string.Format("Your order {0}", id), user.Email, string.Format("{0}, <br />We received your order. For order details click here. <br />Thank you for choosing us.<br />Golex Team.", user.UserName));
                }
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        private void updateHearingSessions()
        {
            var h = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<OrderHub>();

            List<OrderViewModel> sessions = storeDB.orders.Where(o => o.ordered == false | o.ordered == null).OrderByDescending(o => o.id).AsEnumerable().Select(
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
    }
}