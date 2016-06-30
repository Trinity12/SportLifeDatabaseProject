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
using SportLife.Core.Database;
using SportLife.Core.Interfaces;
using SportLife.Website.Areas.AdminOffice.Models;
using FileType = SportLife.Website.Resouses.FileType;

namespace SportLife.Website.Areas.AdminOffice.Controllers {
    public class SportKindsController : Controller {
        private IUnitOfWork _unitOfWork;

        private IUnitOfWork UnitOfWork
                => _unitOfWork ?? (_unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>());

        public ActionResult Index () {
            var sportKindsDb = UnitOfWork.SportCategoryRepository.GetAll();
            var sportKindsVm = Mapper.Map<IEnumerable<SportCategory>, IEnumerable<SportCategoryViewModel>>(sportKindsDb);
            return View(sportKindsVm);
        }

        public ActionResult CreateSportCategory () {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSportCategory ( SportCategoryViewModel category, HttpPostedFileBase upload ) {
            if ( ModelState.IsValid ) {
                var categoryDb = Mapper.Map<SportCategoryViewModel, SportCategory>(category);

                if ( upload != null && upload.ContentLength > 0 ) {
                    var avatar = new Image {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = UnitOfWork.FileTypeRepository.GetByName(FileType.Avatar.ToString()).FileTypeId,
                        ContentType = upload.ContentType
                    };
                    using ( var reader = new System.IO.BinaryReader(upload.InputStream) ) {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    categoryDb.Image1 = avatar;
                }
                UnitOfWork.SportCategoryRepository.Add(categoryDb);
                UnitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: AdminOffice/SportKinds/CreateSportKind
        public ActionResult CreateSportKind () {
            ViewBag.SelectedCategoryId = new SelectList(UnitOfWork.SportCategoryRepository.GetAll(), "SportCategoryId", "SportCategoryName");
            return View();
        }

        // POST: AdminOffice/SportKinds/CreateSportKind
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSportKind ( [Bind(Include = "Name,Categories,SelectedCategoryId")] CreateSportKindViewModel sportKind, HttpPostedFileBase upload ) {
            if ( ModelState.IsValid ) {
                var sportKindDb = Mapper.Map<CreateSportKindViewModel, SportKind>(sportKind);

                if ( upload != null && upload.ContentLength > 0 ) {
                    var avatar = new Image {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = UnitOfWork.FileTypeRepository.GetByName(FileType.Avatar.ToString()).FileTypeId,
                        ContentType = upload.ContentType
                    };
                    using ( var reader = new System.IO.BinaryReader(upload.InputStream) ) {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    sportKindDb.Image1 = avatar;
                }
                UnitOfWork.SportRepository.Add(sportKindDb);
                UnitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SelectedCategoryId = new SelectList(UnitOfWork.SportCategoryRepository.GetAll(), "SportCategoryId", "SportCategoryName", sportKind.SelectedCategoryId);
            return View(sportKind);
        }

        public ActionResult EditCategory ( int? id ) {
            if ( id == null ) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = Mapper.Map<SportCategory, SportCategoryViewModel>(UnitOfWork.SportCategoryRepository.Get(id.Value));
            if ( category == null ) {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory ( SportCategoryViewModel category, HttpPostedFileBase upload ) {
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
                    UnitOfWork.SportCategoryRepository.Get(category.ID).Image1 = avatar;
                }
                UnitOfWork.SportCategoryRepository.Get(category.ID).SportCategoryName = category.Name;
                UnitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult EditSportKind ( int? id ) {
            if ( id == null ) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportKindViewModel sportKind =
                Mapper.Map<SportKind, SportKindViewModel>(UnitOfWork.SportRepository.Get(id.Value));
            if ( sportKind == null ) {
                return HttpNotFound();
            }
            ViewBag.SportCategory = new SelectList(UnitOfWork.SportCategoryRepository.GetAll(), "SportCategoryId", "SportCategoryName", sportKind.SportCategory);
            return View(sportKind);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSportKind ( SportKindViewModel sportKind, HttpPostedFileBase upload ) {
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
                    UnitOfWork.SportRepository.Get(sportKind.ID).Image1 = avatar;
                }
                var sportDb = UnitOfWork.SportRepository.Get(sportKind.ID);
                sportDb.SportName = sportKind.Name;
                sportDb.SportCategoryId = sportKind.SportCategory;
                sportDb.SportCategory = UnitOfWork.SportCategoryRepository.Get(sportDb.SportCategoryId.Value);
                UnitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SportCategory = new SelectList(UnitOfWork.SportCategoryRepository.GetAll(), "SportCategoryId", "SportCategoryName", sportKind.SportCategory);
            return View(sportKind);
        }

        public ActionResult Delete ( int? id ) {
            if ( id == null ) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportKind sportKind = UnitOfWork.SportRepository.Get(id.Value);
            if ( sportKind == null ) {
                return HttpNotFound();
            }
            return View(sportKind);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed ( int id ) {
            SportKind sportKind = UnitOfWork.SportRepository.Get(id);
            UnitOfWork.SportRepository.Remove(sportKind);
            UnitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose ( bool disposing ) {
            if ( disposing ) {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
