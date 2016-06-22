using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using SportLife.Core.Generic;

namespace SportLife.Website.AreasOffices.AdminOffice.Controllers
{
    public class AdminOfficeController : ControllerBase
    {
        public AdminOfficeController () : base() {
            
        }
        // GET: AdminOffice
        public ActionResult Index() {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();
            UnitOfWork

            return View();
        }

        protected override void ExecuteCore () {
            throw new NotImplementedException();
        }
    }
}