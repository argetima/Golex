using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pizza.Startup))]
namespace Pizza
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
