using System.Collections.Generic;
using System.Web.Mvc;
using SportLife.Website.Models.OfficeCommon;

namespace SportLife.Website.Areas.AdminOffice.Controllers {
    public class AdminHomeController : Controller {
        private readonly List<NavItem> _navigation;

        public AdminHomeController () {
            _navigation = new List<NavItem>() {
                new NavItem() { Controller = "Clients", Method = "Index", Label = "Clients" },
                new NavItem() { Controller = "Coaches", Method = "Index", Label = "Coaches" }
            };
        }

        // GET: AdminOffice/Home
        public ActionResult Index () {
            return View();
        }

        public PartialViewResult Navigation () {
            return PartialView("_AdminNavigationAside", _navigation);
        }
    }
}