using System.Web.Mvc;

namespace SportLife.Website.Areas.ClientOffice
{
    public class ClientOfficeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ClientOffice";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ClientOffice_default",
                "ClientOffice/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}