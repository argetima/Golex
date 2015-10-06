using Pizza.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pizza
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            //List<Item> itemList = new List<Item>();
            //itemList.Add(new Item { name = "Margarita", description = "pershute, qepe, djathe", price = Convert.ToDecimal(8.50) });
            ////itemList.Add(new Item { name = "Cheese", description = "djath, qepe, djathe", price = 4.49 });
            ////itemList.Add(new Item { name = "Home", description = "pershute, suxhuk, djathe", price = 9.50 });
            ////itemList.Add(new Item { name = "Donner", description = "doner, qepe, djathe", price = 10.99 });
            ////itemList.Add(new Item { name = "Hamburger", description = "mish, qepe, djathe", price = 5.20 });

            //context.items.AddRange(itemList);

            //context.SaveChanges();
        }
    }

}