using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;
using SportLife.Models.IdentityModels;
using SportLife.Website.Areas.AdminOffice.Models;
using SportLife.Website.Resouses;

namespace SportLife.Website.Areas.AdminOffice.Controllers
{
    public class ClientsController : Controller {
        private IUnitOfWork _unitOfWork;
        private MyUserManager _userManager;
        private RoleManager _roleManager;

        public MyUserManager UserManager {
            get {
                return _userManager ?? (_userManager = HttpContext.GetOwinContext().GetUserManager<MyUserManager>());
            }
            private set {
                _userManager = value;
            }
        }

        private RoleManager RoleManager
            => _roleManager ?? (_roleManager = HttpContext.GetOwinContext().Get<RoleManager>());
        private IUnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>());

        // GET: AdminOffice/Clients
        public ActionResult Index() {
            var clientModel = UnitOfWork.ClientRepository.GetAll();
            var client = Mapper.Map<IEnumerable<Client>, IEnumerable<ClientViewModel>>(clientModel);
            return View(client.ToList());
        }

        // GET: AdminOffice/Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = Mapper.Map<Client, ClientFullViewModel>( UnitOfWork.ClientRepository.Get(id.Value));

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        public Task<ActionResult> AddToRole ( int userId, MainRoles role ) {
            if ( RoleManager.FindByNameAsync(role.ToString()) == null ) {
                var roleInstanse = new SportLife.Models.IdentityModels.Role();
                roleInstanse.Name = role.ToString();

                var result = RoleManager.CreateAsync(roleInstanse);
                //if (!result.Result.Succeeded)
                //    return new 
            } else if ( !UserManager.IsInRoleAsync(userId, role.ToString()).Result )
                if ( !UserManager.AddToRoleAsync(userId, role.ToString()).Result.Succeeded )
                    return RedirectToAction("Details", new { id = userId});
        }
    }
}
