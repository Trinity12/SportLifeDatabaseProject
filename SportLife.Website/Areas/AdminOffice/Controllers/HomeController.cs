using System.Collections.Generic;
using System.Web.Mvc;
using SportLife.Website.Models.OfficeCommon;

namespace SportLife.Website.Areas.AdminOffice.Controllers {
    public class AdminHomeController : Controller {
        private readonly List<NavItem> _navigation;

        public AdminHomeController () {
            _navigation = new List<NavItem>() {
                new NavItem() { Controller = "Clients", Method = "Index", Label = "Clients" },
                new NavItem() { Controller = "Coaches", Method = "Index", Label = "Coaches" },
                new NavItem() { Controller = "SportKinds", Method = "Index", Label = "Sport kinds and categories" },
                new NavItem() { Controller = "Halls", Method = "Index", Label = "Halls" },
                new NavItem() { Controller = "Shedules", Method = "Index", Label = "Shedule" },
                new NavItem() { Controller = "SportGroups", Method = "Index", Label = "Groups" },
                new NavItem() { Controller = "Abonements", Method = "Index", Label = "Abonements" },
                new NavItem() { Controller = "Visitings", Method = "Index", Label = "Visitings" },
                new NavItem() { Controller = "AbonementOrders", Method = "Index", Label = "Abonement Orders" },
                new NavItem() { Controller = "Prices", Method = "Index", Label = "Prices" }
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