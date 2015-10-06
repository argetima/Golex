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
    public class ItemsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        //
        // GET: /Items/
        public ActionResult List()
        {
            return View(db.items);
        }

        public void updateHearingSessions()
        {
            var h = GlobalHost.ConnectionManager.GetHubContext<OrderHub>();
            
            List<OrderViewModel> sessions = db.orders.Where(o => o.ordered == false | o.ordered == null).Select(
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

        //
        // GET: /Items/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Items/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Items/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Items/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Items/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Items/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Items/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
