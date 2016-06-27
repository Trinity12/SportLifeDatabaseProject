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

namespace SportLife.Website.Areas.AdminOffice.Controllers {
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
        public ActionResult Index () {
            var clientModel = UnitOfWork.ClientRepository.GetAll();
            var client = Mapper.Map<IEnumerable<Client>, IEnumerable<ClientViewModel>>(clientModel);
            return View(client.ToList());
        }

        // GET: AdminOffice/Clients/Details/5
        public ActionResult Details ( int? id, OperationSuccess message = OperationSuccess.Default ) {
            ViewBag.StatusMessage =
                message == OperationSuccess.Success
                    ? _successMesage
                    : message == OperationSuccess.Fail
                        ? _failMesage
                        : string.Empty;

            if ( id == null ) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = Mapper.Map<Client, ClientFullViewModel>(UnitOfWork.ClientRepository.Get(id.Value));

            if ( client == null ) {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        public ActionResult AddToRole ( int userId, MainRoles role ) {
            var message = OperationSuccess.Default;

            if ( RoleManager.FindByNameAsync(role.ToString()) == null ) {
                var roleInstanse = new SportLife.Models.IdentityModels.Role();
                roleInstanse.Name = role.ToString();

                var result = RoleManager.CreateAsync(roleInstanse);
                if ( !result.Result.Succeeded )
                    message = OperationSuccess.Fail;
            } else
                if ( !UserManager.IsInRoleAsync(userId, role.ToString()).Result )
                message = !UserManager.AddToRoleAsync(userId, role.ToString()).Result.Succeeded
                    ? OperationSuccess.Fail
                    : OperationSuccess.Success;

            return RedirectToAction("Details", new { userId, message });
        }

        #region Helpers and resourses

        public enum OperationSuccess {
            Success,
            Fail,
            Default
        }

        private const string _successMesage = "Your operation has being finished successfully!";
        private const string _failMesage = "There is an error! Your operation hasn't being finished successfully!";

        #endregion

    }

}
