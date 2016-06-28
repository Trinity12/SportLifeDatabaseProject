using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;
using SportLife.Models.IdentityModels;
using SportLife.Website.Areas.AdminOffice.Models;
using SportLife.Website.Resouses;
using FileType = SportLife.Website.Resouses.FileType;

namespace SportLife.Website.Areas.AdminOffice.Controllers {
    public class CoachesController : Controller {
        private IUnitOfWork _unitOfWork;
        private MyUserManager _userManager;
        private RoleManager _roleManager;

        public MyUserManager UserManager 
            => _userManager ?? (_userManager = HttpContext.GetOwinContext().GetUserManager<MyUserManager>());

        private RoleManager RoleManager
            => _roleManager ?? (_roleManager = HttpContext.GetOwinContext().Get<RoleManager>());

        private IUnitOfWork UnitOfWork
            => _unitOfWork ?? (_unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>());
       

        // GET: AdminOffice/Coaches
        public ActionResult Index () {
            var coaches = Mapper.Map<IEnumerable<Coach>, IEnumerable<CoachViewModel>>(UnitOfWork.CoachRepository.GetAll());
            return View(coaches.ToList());
        }

        // GET: AdminOffice/Coaches/Details/5
        public ActionResult Details ( int? id, OperationSuccess message = OperationSuccess.Default ) {
            if ( id == null ) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var coach = Mapper.Map<Coach, CoachFullViewModel>(UnitOfWork.CoachRepository.Get(id.Value));
            if ( coach == null ) {
                return HttpNotFound();
            }
            return View(coach);
        }

        // GET: AdminOffice/Coaches/Edit/5
        public ActionResult AddAvatar ( int? id ) {
            if ( id == null ) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var coach = new CoachEditViewModel() { ID = id.Value };
            return View(coach);
        }

        // POST: AdminOffice/Coaches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAvatar ( CoachEditViewModel coach, HttpPostedFileBase upload ) {
            if ( ModelState.IsValid ) {
                if ( upload != null && upload.ContentLength > 0 ) {
                    var avatar = new Image {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = UnitOfWork.FileTypeRepository.GetByName(FileType.Avatar.ToString()).FileTypeId,
                        ContentType = upload.ContentType
                    };
                    using ( var reader = new System.IO.BinaryReader(upload.InputStream) ) {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    UnitOfWork.UserRepository.Get(coach.ID).Image.Add(avatar);
                    UnitOfWork.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(coach);
        }

        [HttpPost]
        public ActionResult AddToRole ( int userId, MainRoles role ) {
            var message = OperationSuccess.Default;
            if ( RoleManager.FindByNameAsync(role.ToString()).Result == null ) {
                var roleInstanse = new SportLife.Models.IdentityModels.Role();
                roleInstanse.Name = role.ToString();

                var result = RoleManager.CreateAsync(roleInstanse);
                if ( !result.Result.Succeeded )
                    message = OperationSuccess.Fail;
            }
            if (!UserManager.IsInRoleAsync(userId, role.ToString()).Result)
                message = !UserManager.AddToRoleAsync(userId, role.ToString()).Result.Succeeded
                    ? OperationSuccess.Fail
                    : OperationSuccess.Success;
            else message = OperationSuccess.Success;

            if (role == MainRoles.Coach && message == OperationSuccess.Success)
            {
                UnitOfWork.CoachRepository.Add(userId);
            }

            return RedirectToAction("Details", new { id = userId, message });
        }

        protected override void Dispose ( bool disposing ) {
            if ( disposing ) {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Helpers and resourses

        private const string _successMesage = "Your operation has being finished successfully!";
        private const string _failMesage = "There is an error! Your operation hasn't being finished successfully!";

        #endregion
    }
}
