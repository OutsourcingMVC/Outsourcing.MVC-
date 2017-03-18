using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Outsourcing.MVC.Startup))]
namespace Outsourcing.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ///ConfigureAuth(app);
        }
    }
}
