using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Pizza.Models;

namespace Pizza.Hubs
{
    public class OrderHub : Hub
    {
        public void getSessions()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<OrderViewModel> sessions = db.orders.Where(o => o.ordered == false | o.ordered == null).OrderByDescending(o => o.id).AsEnumerable().Select(
                o => new OrderViewModel
                {
                    id = o.id,
                    phone = o.Phone,
                    adress = o.Address,
                    datetime = Pizza.Controllers.Extensions.Extensions.datetimeToString(o.datetime),
                    sum = o.Total.ToString("0.00")
                }).ToList();

                Clients.Caller.updateSessions(sessions);
            }
        }
    }
}