using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Ectocyst
{
    public class EctocystAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Ectocyst";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Ectocyst_default",
                "Ectocyst/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}