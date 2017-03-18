using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Personal
{
    public class PersonalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Personal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Personal_default",
                "Personal/{controller}/{action}/{id}",
                new {area= "Personal", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}