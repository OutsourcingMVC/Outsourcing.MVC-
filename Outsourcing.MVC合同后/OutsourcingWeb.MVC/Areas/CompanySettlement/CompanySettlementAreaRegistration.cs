using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.CompanySettlement
{
    public class CompanySettlementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CompanySettlement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CompanySettlement_default",
                "CompanySettlement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}