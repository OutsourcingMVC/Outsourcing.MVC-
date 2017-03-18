using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.CooperativeContractA
{
    public class CooperativeContractAAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CooperativeContractA";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CooperativeContractA_default",
                "CooperativeContractA/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}