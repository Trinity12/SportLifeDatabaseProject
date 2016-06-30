using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportLife.Core.Database;

namespace SportLife.Website.Areas.AdminOffice.Controllers
{
    public class VisitingsController : Controller
    {
        private SportLifeEntities db = new SportLifeEntities();

        // GET: AdminOffice/Visitings
        public ActionResult Index()
        {
            var visiting = db.Visiting.Include(v => v.Client).Include(v => v.Shedule);
            return View(visiting.ToList());
        }

        // GET: AdminOffice/Visitings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visiting visiting = db.Visiting.Find(id);
            if (visiting == null)
            {
                return HttpNotFound();
            }
            return View(visiting);
        }

        // GET: AdminOffice/Visitings/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Client, "ClientId", "ClientId");
            ViewBag.SheduleId = new SelectList(db.Shedule, "ShedulId", "SheduleDay");
            return View();
        }

        // POST: AdminOffice/Visitings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VisitingId,VisitingDateTime,SheduleId,ClientId")] Visiting visiting)
        {
            if (ModelState.IsValid)
            {
                db.Visiting.Add(visiting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Client, "ClientId", "ClientId", visiting.ClientId);
            ViewBag.SheduleId = new SelectList(db.Shedule, "ShedulId", "SheduleDay", visiting.SheduleId);
            return View(visiting);
        }

        // GET: AdminOffice/Visitings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visiting visiting = db.Visiting.Find(id);
            if (visiting == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Client, "ClientId", "ClientId", visiting.ClientId);
            ViewBag.SheduleId = new SelectList(db.Shedule, "ShedulId", "SheduleDay", visiting.SheduleId);
            return View(visiting);
        }

        // POST: AdminOffice/Visitings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VisitingId,VisitingDateTime,SheduleId,ClientId")] Visiting visiting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visiting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Client, "ClientId", "ClientId", visiting.ClientId);
            ViewBag.SheduleId = new SelectList(db.Shedule, "ShedulId", "SheduleDay", visiting.SheduleId);
            return View(visiting);
        }

        // GET: AdminOffice/Visitings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visiting visiting = db.Visiting.Find(id);
            if (visiting == null)
            {
                return HttpNotFound();
            }
            return View(visiting);
        }

        // POST: AdminOffice/Visitings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visiting visiting = db.Visiting.Find(id);
            db.Visiting.Remove(visiting);
            db.SaveChanges();
            return RedirectToAction("Index");
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
