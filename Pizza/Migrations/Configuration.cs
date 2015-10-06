namespace Pizza.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Pizza.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "pizza";
        }

        protected override void Seed(DatabaseContext context)
        {
            List<Item> itemList = new List<Item>();
            itemList.Add(new Item { name = "Margarita", description = "pershute, qepe, djathe", price = Convert.ToDecimal(8.50) });
            //itemList.Add(new Item { name = "Cheese", description = "djath, qepe, djathe", price = 4.49 });
            //itemList.Add(new Item { name = "Home", description = "pershute, suxhuk, djathe", price = 9.50 });
            //itemList.Add(new Item { name = "Donner", description = "doner, qepe, djathe", price = 10.99 });
            //itemList.Add(new Item { name = "Hamburger", description = "mish, qepe, djathe", price = 5.20 });

            context.items.AddRange(itemList);

        }
    }
}
