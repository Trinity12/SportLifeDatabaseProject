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
    public class PricesController : Controller
    {
        private SportLifeEntities db = new SportLifeEntities();

        // GET: AdminOffice/Prices
        public ActionResult Index()
        {
            var price = db.Price.Include(p => p.Abonement);
            return View(price.ToList());
        }

        // GET: AdminOffice/Prices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = db.Price.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        // GET: AdminOffice/Prices/Create
        public ActionResult Create()
        {
            ViewBag.AbonementId = new SelectList(db.Abonement, "AbonementId", "AbonementName");
            return View();
        }

        // POST: AdminOffice/Prices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PriceId,Price1,AbonementId")] Price price)
        {
            if (ModelState.IsValid)
            {
                db.Price.Add(price);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AbonementId = new SelectList(db.Abonement, "AbonementId", "AbonementName", price.AbonementId);
            return View(price);
        }

        // GET: AdminOffice/Prices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = db.Price.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            ViewBag.AbonementId = new SelectList(db.Abonement, "AbonementId", "AbonementName", price.AbonementId);
            return View(price);
        }

        // POST: AdminOffice/Prices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PriceId,Price1,AbonementId")] Price price)
        {
            if (ModelState.IsValid)
            {
                db.Entry(price).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AbonementId = new SelectList(db.Abonement, "AbonementId", "AbonementName", price.AbonementId);
            return View(price);
        }

        // GET: AdminOffice/Prices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price price = db.Price.Find(id);
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        // POST: AdminOffice/Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Price price = db.Price.Find(id);
            db.Price.Remove(price);
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
