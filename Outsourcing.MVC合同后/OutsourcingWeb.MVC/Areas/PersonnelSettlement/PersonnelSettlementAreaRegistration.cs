using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.PersonnelSettlement
{
    public class PersonnelSettlementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PersonnelSettlement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PersonnelSettlement_default",
                "PersonnelSettlement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}