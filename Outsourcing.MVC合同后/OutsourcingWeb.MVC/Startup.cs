using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OutsourcingWeb.MVC.Startup))]
namespace OutsourcingWeb.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
