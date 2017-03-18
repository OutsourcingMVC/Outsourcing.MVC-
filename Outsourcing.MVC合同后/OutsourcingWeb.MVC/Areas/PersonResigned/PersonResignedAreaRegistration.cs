using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.PersonResigned
{
    public class PersonResignedAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PersonResigned";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PersonResigned_default",
                "PersonResigned/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}