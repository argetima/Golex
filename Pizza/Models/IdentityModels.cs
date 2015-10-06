using Microsoft.AspNet.Identity.EntityFramework;

namespace Pizza.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool active { get; set; }
        public string address { get; set; }
        public string place { get; set; }
        public string phone { get; set; }
        //public string email { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Id, UserName);
        }
    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("storeUsers")
    //    {
    //    }
    //}
}