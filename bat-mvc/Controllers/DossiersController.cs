using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace bat_mvc.Controllers
{
    [Authorize]
    public class DossiersController : Controller
    {
        public readonly IRepository<Dossier> _dossierRepo;
        public readonly IUnitOfWork _uow;

        public DossiersController(IRepository<Dossier> dossierRepository, IUnitOfWork uow)
        {
            _dossierRepo = dossierRepository;
            _uow = uow;
        }

        // GET: Dossiers
        public ActionResult Index()
        {
            var dossiers = _uow.Dossiers.GetAll();  //.GetSomeDossiers(3));
            return View(dossiers);
        }

        // GET: Dossiers/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //Dossier Dossier = db.Dossiers.Find(id);
        //    if (Dossier == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Dossier);
        //}

        // GET: Dossiers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dossiers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "DossierID,DossierName")] Dossier Dossier)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Dossiers.Add(Dossier);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(Dossier);
        //}

        // GET: Dossiers/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Dossier Dossier = db.Dossiers.Find(id);
        //    if (Dossier == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Dossier);
        //}

        // POST: Dossiers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "DossierID,DossierName")] Dossier Dossier)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(Dossier).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(Dossier);
        //}

        // GET: Dossiers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Dossier Dossier = db.Dossiers.Find(id);
        //    if (Dossier == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Dossier);
        //}

        // POST: Dossiers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Dossier Dossier = db.Dossiers.Find(id);
        //    db.Dossiers.Remove(Dossier);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
