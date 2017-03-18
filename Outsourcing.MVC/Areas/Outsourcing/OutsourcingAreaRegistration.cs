using System.Web.Mvc;

namespace Outsourcing.MVC.Areas.Outsourcing
{
    public class OutsourcingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Outsourcing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Outsourcing_default",
                "Outsourcing/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}