using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.PersonEntry
{
    public class PersonEntryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PersonEntry";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PersonEntry_default",
                "PersonEntry/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}