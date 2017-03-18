using System.Web.Mvc;

namespace Outsourcing.MVC.Areas.LieTou
{
    public class LieTouAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LieTou";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LieTou_default",
                "LieTou/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}