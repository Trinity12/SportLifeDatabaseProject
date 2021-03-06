﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public MyUserManager UserManager
        {
            get
            {
                return _userManager ?? (_userManager = HttpContext.GetOwinContext().GetUserManager<MyUserManager>());
            }
            private set
            {
                _userManager = value;
            }
        }

        private RoleManager RoleManager
            => _roleManager ?? (_roleManager = HttpContext.GetOwinContext().Get<RoleManager>());
        private IUnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>());

        // GET: AdminOffice/Clients
        public ActionResult Index () {
            var clientModel = UnitOfWork.ClientRepository.GetAll();
            var clients = Mapper.Map<IEnumerable<Client>, List<ClientViewModel>>(clientModel);
            return View(clients);
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

        public ActionResult AddToRole ( int userId, MainRoles role ) {
            var message = OperationSuccess.Default;
            if ( RoleManager.FindByNameAsync(role.ToString()).Result == null ) {
                var roleInstanse = new SportLife.Models.IdentityModels.Role();
                roleInstanse.Name = role.ToString();

                var result = RoleManager.CreateAsync(roleInstanse);
                if ( !result.Result.Succeeded )
                    message = OperationSuccess.Fail;
            }
            if ( !UserManager.IsInRoleAsync(userId, role.ToString()).Result )
                message = !UserManager.AddToRoleAsync(userId, role.ToString()).Result.Succeeded
                    ? OperationSuccess.Fail
                    : OperationSuccess.Success;
            else message = OperationSuccess.Success;

            if ( role == MainRoles.Coach && message == OperationSuccess.Success ) {
                UnitOfWork.CoachRepository.Add(userId);
                UnitOfWork.SaveChanges();
            }

            return RedirectToAction("Details", new { id = userId, message });
        }

        public PartialViewResult ClientSearchBySurname ( string surname )
        {
            var result = UnitOfWork.ClientRepository.Find(client => client.User.UserSurname == surname);
            return PartialView("_ClientSearch", Mapper.Map<IEnumerable<Client>, IEnumerable<ClientViewModel>>(result));
        }

        public PartialViewResult ClientSearchByEmail ( string name ) {
            var result = UnitOfWork.ClientRepository.Find(client => client.User.UserFirstName == name);
            return PartialView("_ClientSearch", Mapper.Map<IEnumerable<Client>, IEnumerable<ClientViewModel>>(result));
        }

        public PartialViewResult ClientSearchByPhoneNumber ( string phone ) {
            var result = UnitOfWork.ClientRepository.Find(client => client.User.Email == phone);
            return PartialView("_ClientSearch", Mapper.Map<IEnumerable<Client>, IEnumerable<ClientViewModel>>(result));
        }

        #region Helpers and resourses

        private const string _successMesage = "Your operation has being finished successfully!";
        private const string _failMesage = "There is an error! Your operation hasn't being finished successfully!";

        #endregion

    }

}
