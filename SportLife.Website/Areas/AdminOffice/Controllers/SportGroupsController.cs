using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using AutoMapper;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;
using SportLife.Website.Areas.AdminOffice.Models;

namespace SportLife.Website.Areas.AdminOffice.Controllers
{
    public class SportGroupsController : Controller
    {
        private IUnitOfWork _unitOfWork;

        private IUnitOfWork UnitOfWork
                => _unitOfWork ?? (_unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>());

        // GET: AdminOffice/SportGroups
        public ActionResult Index()
        {
            var sportGroup = UnitOfWork.SportGroupRepository.GetAll();
            var sportGroupVm = Mapper.Map<IEnumerable<SportGroup>, IEnumerable<GroupListViewModel>>(sportGroup);
            return View(sportGroupVm);
        }

        // GET: AdminOffice/SportGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sportGroup = Mapper.Map<SportGroup, GroupViewModel>(UnitOfWork.SportGroupRepository.Get(id.Value));
            if (sportGroup == null)
            {
                return HttpNotFound();
            }
            return View(sportGroup);
        }

        // GET: AdminOffice/SportGroups/Create
        public ActionResult Create()
        {
            var coaches =
                Mapper.Map<IEnumerable<Coach>, IEnumerable<CoachDropDownViewModel>>(UnitOfWork.CoachRepository.GetAll());
            var sportKinds =
                Mapper.Map<IEnumerable<SportKind>, IEnumerable<SportDropDownViewModel>>(UnitOfWork.SportRepository.GetAll());
            var halls =
                Mapper.Map<IEnumerable<Hall>, IEnumerable<HallDropDownViewModel>>(UnitOfWork.HallRepository.GetAll());
            ViewBag.CoachId = new SelectList(coaches, "ID", "FullName");
            ViewBag.SheduleDayId = new SelectList(EnumHelper.GetSelectList(typeof(DayOfWeek)), DayOfWeek.Monday);
            ViewBag.HallId = new SelectList(halls, "ID", "FullAdress");
            ViewBag.SportId = new SelectList(sportKinds, "SportId", "SportName");
            return View();
        }

        // POST: AdminOffice/SportGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoachId,SportId,GroupMaxMembers,SheduleDayId,SheduleTime,HallId")] CreateGroupViewModel sportGroup)
        {
            if (ModelState.IsValid)
            {
                var group = Mapper.Map<CreateGroupViewModel, SportGroup>(sportGroup);
                var shedule = new Shedule();
                shedule.SheduleDayId = sportGroup.SheduleDayId;
                shedule.SportGroup = group;
                shedule.SheduleTime = sportGroup.SheduleTime;
                shedule.HallId = sportGroup.HallId;
                UnitOfWork.SheduleRepository.Add(shedule);
                UnitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            var coaches =
                Mapper.Map<IEnumerable<Coach>, IEnumerable<CoachDropDownViewModel>>(UnitOfWork.CoachRepository.GetAll());
            var sportKinds =
                Mapper.Map<IEnumerable<SportKind>, IEnumerable<SportDropDownViewModel>>(UnitOfWork.SportRepository.GetAll());
            var halls =
                Mapper.Map<IEnumerable<Hall>, IEnumerable<HallDropDownViewModel>>(UnitOfWork.HallRepository.GetAll());
            ViewBag.CoachId = new SelectList(coaches, "ID", "FullName");
            ViewBag.SheduleDayId = new SelectList(EnumHelper.GetSelectList(typeof(DayOfWeek)), DayOfWeek.Monday);
            ViewBag.HallId = new SelectList(halls, "ID", "FullAdress");
            ViewBag.SportId = new SelectList(sportKinds, "SportId", "SportName");
            return View(sportGroup);
        }

        // GET: AdminOffice/SportGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sportGroup = Mapper.Map<SportGroup, GroupEditViewModel>(UnitOfWork.SportGroupRepository.Get(id.Value));
            if (sportGroup == null)
            {
                return HttpNotFound();
            }
            var coaches =
                Mapper.Map<IEnumerable<Coach>, IEnumerable<CoachDropDownViewModel>>(UnitOfWork.CoachRepository.GetAll());
            ViewBag.CoachId = new SelectList(coaches, "ID", "FullName");
            return View(sportGroup);
        }

        // POST: AdminOffice/SportGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GroupMaxMembers,CoachId")] GroupEditViewModel sportGroup)
        {
            if (ModelState.IsValid)
            {
                var group = UnitOfWork.SportGroupRepository.Get(sportGroup.ID);
                group.CoachId = sportGroup.CoachId;
                group.GroupMaxMembers = sportGroup.GroupMaxMembers;
                UnitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            var coaches =
                Mapper.Map<IEnumerable<Coach>, IEnumerable<CoachDropDownViewModel>>(UnitOfWork.CoachRepository.GetAll());
            ViewBag.CoachId = new SelectList(coaches, "ID", "FullName");
            return View(sportGroup);
        }

        // GET: AdminOffice/SportGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportGroup sportGroup = UnitOfWork.SportGroupRepository.Get(id.Value);
            if (sportGroup == null)
            {
                return HttpNotFound();
            }
            return View(sportGroup);
        }

        // POST: AdminOffice/SportGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SportGroup sportGroup = UnitOfWork.SportGroupRepository.Get(id);
            UnitOfWork.SportGroupRepository.Remove(sportGroup);
            UnitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult RemoveClient(int id)
        {
            var group = UnitOfWork.ClientRepository.Get(id).SportGroup;
            group.Client.Remove(UnitOfWork.ClientRepository.Get(id));
            UnitOfWork.SaveChanges();

            return RedirectToAction("Details", id = group.GroupId);
        }
    }
}
