using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using AutoMapper;
using SportLife.Core.Database;
using SportLife.Core.Interfaces;
using SportLife.Website.Areas.AdminOffice.Models;
using SportLife.Website.Resouses;

namespace SportLife.Website.Areas.AdminOffice.Controllers
{
    public class ShedulesController : Controller
    {
        private SportLifeEntities db = new SportLifeEntities();
        private IUnitOfWork _unitOfWork;

        private IUnitOfWork UnitOfWork
                => _unitOfWork ?? (_unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>());

        // GET: AdminOffice/Shedules
        public ActionResult Index()
        {
            var shedule =
                UnitOfWork.SheduleRepository.GetAll()
                    .OrderBy(shed => shed.SheduleDayId.Value)
                    .ThenBy(shed => shed.SheduleTime);
            var sheduleVm = Mapper.Map<IEnumerable<Shedule>, IEnumerable<SheduleAdminViewModel>>(shedule);
            return View(sheduleVm);
        }

        // GET: AdminOffice/Shedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shedule = Mapper.Map<Shedule, SheduleEditViewModel>(UnitOfWork.SheduleRepository.Get(id.Value));
            if (shedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.SheduleDayId = new SelectList(EnumHelper.GetSelectList(typeof(DayOfWeek)), shedule.SheduleDayId);
            return View(shedule);
        }

        // POST: AdminOffice/Shedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShedulId,SheduleDay,SheduleDayId,SheduleTime")] SheduleEditViewModel shedule)
        {
            if (ModelState.IsValid)
            {
                var sheduleDb = UnitOfWork.SheduleRepository.Get(shedule.ShedulId);
                sheduleDb.SheduleDayId = shedule.SheduleDayId;
                sheduleDb.SheduleTime = shedule.SheduleTime;
                UnitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SheduleDayId = new SelectList(EnumHelper.GetSelectList(typeof(DaysInWeek)), shedule.SheduleDayId);
            return View(shedule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
