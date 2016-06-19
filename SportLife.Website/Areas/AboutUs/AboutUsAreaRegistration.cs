using System.Web.Mvc;

namespace SportLife.Website.Areas.AboutUs
{
    public class AboutUsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AboutUs";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AboutUs_default",
                "AboutUs/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}