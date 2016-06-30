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
    public class AbonementOrdersController : Controller
    {
        private SportLifeEntities db = new SportLifeEntities();

        // GET: AdminOffice/AbonementOrders
        public ActionResult Index()
        {
            var abonementOrder = db.AbonementOrder.Include(a => a.Client).Include(a => a.Price);
            return View(abonementOrder.ToList());
        }

        // GET: AdminOffice/AbonementOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbonementOrder abonementOrder = db.AbonementOrder.Find(id);
            if (abonementOrder == null)
            {
                return HttpNotFound();
            }
            return View(abonementOrder);
        }

        // GET: AdminOffice/AbonementOrders/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Client, "ClientId", "ClientId");
            ViewBag.PriceId = new SelectList(db.Price, "PriceId", "PriceId");
            return View();
        }

        // POST: AdminOffice/AbonementOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,PriceId,ClientId")] AbonementOrder abonementOrder)
        {
            if (ModelState.IsValid)
            {
                db.AbonementOrder.Add(abonementOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Client, "ClientId", "ClientId", abonementOrder.ClientId);
            ViewBag.PriceId = new SelectList(db.Price, "PriceId", "PriceId", abonementOrder.PriceId);
            return View(abonementOrder);
        }

        // GET: AdminOffice/AbonementOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbonementOrder abonementOrder = db.AbonementOrder.Find(id);
            if (abonementOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Client, "ClientId", "ClientId", abonementOrder.ClientId);
            ViewBag.PriceId = new SelectList(db.Price, "PriceId", "PriceId", abonementOrder.PriceId);
            return View(abonementOrder);
        }

        // POST: AdminOffice/AbonementOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,PriceId,ClientId")] AbonementOrder abonementOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abonementOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Client, "ClientId", "ClientId", abonementOrder.ClientId);
            ViewBag.PriceId = new SelectList(db.Price, "PriceId", "PriceId", abonementOrder.PriceId);
            return View(abonementOrder);
        }

        // GET: AdminOffice/AbonementOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbonementOrder abonementOrder = db.AbonementOrder.Find(id);
            if (abonementOrder == null)
            {
                return HttpNotFound();
            }
            return View(abonementOrder);
        }

        // POST: AdminOffice/AbonementOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AbonementOrder abonementOrder = db.AbonementOrder.Find(id);
            db.AbonementOrder.Remove(abonementOrder);
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
