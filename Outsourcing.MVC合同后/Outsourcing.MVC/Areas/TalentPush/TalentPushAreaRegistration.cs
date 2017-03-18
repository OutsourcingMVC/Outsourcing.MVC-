using System.Web.Mvc;

namespace Outsourcing.MVC.Areas.TalentPush
{
    public class TalentPushAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TalentPush";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TalentPush_default",
                "TalentPush/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}