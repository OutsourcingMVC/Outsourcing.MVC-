using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.InvoiceManagement
{
    public class InvoiceManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "InvoiceManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "InvoiceManagement_default",
                "InvoiceManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}