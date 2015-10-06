using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Pizza.Models;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Pizza
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Item> items { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
        public DbSet<PaymentMethod> paymentMethod { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DatabaseContext() : base("pizza") { }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            base.OnModelCreating(mb);
            // Code here
        }
    }
}