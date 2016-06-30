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
    public class AbonementsController : Controller
    {
        private SportLifeEntities db = new SportLifeEntities();

        // GET: AdminOffice/Abonements
        public ActionResult Index()
        {
            return View(db.Abonement.ToList());
        }

        // GET: AdminOffice/Abonements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonement abonement = db.Abonement.Find(id);
            if (abonement == null)
            {
                return HttpNotFound();
            }
            return View(abonement);
        }

        // GET: AdminOffice/Abonements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminOffice/Abonements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AbonementId,AbonementName,AbonementDuring,AbonementTrainings")] Abonement abonement)
        {
            if (ModelState.IsValid)
            {
                db.Abonement.Add(abonement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(abonement);
        }

        // GET: AdminOffice/Abonements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonement abonement = db.Abonement.Find(id);
            if (abonement == null)
            {
                return HttpNotFound();
            }
            return View(abonement);
        }

        // POST: AdminOffice/Abonements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AbonementId,AbonementName,AbonementDuring,AbonementTrainings")] Abonement abonement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abonement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(abonement);
        }

        // GET: AdminOffice/Abonements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonement abonement = db.Abonement.Find(id);
            if (abonement == null)
            {
                return HttpNotFound();
            }
            return View(abonement);
        }

        // POST: AdminOffice/Abonements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Abonement abonement = db.Abonement.Find(id);
            db.Abonement.Remove(abonement);
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
