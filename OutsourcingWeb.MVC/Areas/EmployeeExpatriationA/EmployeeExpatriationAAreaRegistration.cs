using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.EmployeeExpatriationA
{
    public class EmployeeExpatriationAAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EmployeeExpatriationA";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EmployeeExpatriationA_default",
                "EmployeeExpatriationA/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}