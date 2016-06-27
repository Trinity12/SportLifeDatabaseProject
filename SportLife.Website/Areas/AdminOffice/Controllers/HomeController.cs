using System.Collections.Generic;
using System.Web.Mvc;
using SportLife.Website.Models.OfficeCommon;

namespace SportLife.Website.Areas.AdminOffice.Controllers
{
    public class AdminHomeController : Controller
    {
        private List<NavItem> _navigation;

        // GET: AdminOffice/Home
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Navigation () {
            _navigation = new List<NavItem>() {
                new NavItem() {Controller = "Clients", Method = "Index", Label = "Clients"}
            };
            return PartialView("_AdminNavigationAside", _navigation);
        }
    }
}