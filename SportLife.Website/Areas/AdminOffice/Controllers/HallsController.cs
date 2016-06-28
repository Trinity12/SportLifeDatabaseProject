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

namespace SportLife.Website.Areas.AdminOffice.Controllers
{
    public class HallsController : Controller
    {
        private IUnitOfWork _unitOfWork;

        private IUnitOfWork UnitOfWork
                => _unitOfWork ?? (_unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>());

        // GET: AdminOffice/Halls
        public ActionResult Index()
        {
            var hall = UnitOfWork.HallRepository.GetAll();
            return View(hall.ToList());
        }

        // GET: AdminOffice/Halls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hallDb = UnitOfWork.HallRepository.Get(id.Value);
            var hall = Mapper.Map<Hall,HallViewModel>(hallDb);
            hall.Adress = Mapper.Map<Adress, AdressViewModel>(hallDb.Adress);
            return View(hall);
        }

        // GET: AdminOffice/Halls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminOffice/Halls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HallId,AdressId,Image")] HallViewModel hall, HttpPostedFileBase upload )
        {
            if (ModelState.IsValid)
            {
                var hallDb = new Hall();
                hallDb.Adress = Mapper.Map<AdressViewModel, Adress>(hall.Adress);
                if ( upload != null && upload.ContentLength > 0 ) {
                    var avatar = new Image {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = UnitOfWork.FileTypeRepository.GetByName(FileType.Avatar.ToString()).FileTypeId,
                        ContentType = upload.ContentType
                    };
                    using ( var reader = new System.IO.BinaryReader(upload.InputStream) ) {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    hallDb.Image1 = avatar;
                }
                UnitOfWork.HallRepository.Add(hallDb);
                UnitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(hall);
        }

        // GET: AdminOffice/Halls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hallDb = UnitOfWork.HallRepository.Get(id.Value);
            var hall = Mapper.Map<Hall, HallViewModel>(hallDb);
            hall.Adress = Mapper.Map<Adress, AdressViewModel>(hallDb.Adress);
            
            return View(hall);
        }

        // POST: AdminOffice/Halls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HallId,AdressId,Image")] HallViewModel hall, HttpPostedFileBase upload )
        {
            if (ModelState.IsValid)
            {
                var hallDb = UnitOfWork.HallRepository.Get(hall.ID);
                hallDb.Adress = Mapper.Map<AdressViewModel, Adress>(hall.Adress);
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new Image
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = UnitOfWork.FileTypeRepository.GetByName(FileType.Avatar.ToString()).FileTypeId,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    hallDb.Image1 = avatar;
                }
                UnitOfWork.SaveChanges();
            }
            return View(hall);
        }

        // GET: AdminOffice/Halls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = UnitOfWork.HallRepository.Get(id.Value);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }

        // POST: AdminOffice/Halls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hall hall = UnitOfWork.HallRepository.Get(id.Value);
            UnitOfWork.HallRepository.Remove(hall);
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
    }
}
