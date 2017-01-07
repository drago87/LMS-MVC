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
    public class ClassUnitsController : Controller
    {
        public readonly IRepository<Subject> _classunitRepo;
        public readonly IUnitOfWork _uow;

        public ClassUnitsController(IRepository<Subject> classunitRepository, IUnitOfWork uow)
        {
            _classunitRepo = classunitRepository;
            _uow = uow;
        }

        // GET: ClassUnits
        public ActionResult Index()
        {
            var classunits = _uow.Classunits.GetAll();
            return View(classunits);
        }

        // GET: ClassUnits/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //Subject classunit = db.ClassUnits.Find(id);
        //    if (classunit == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(classunit);
        //}

        // GET: ClassUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "SubjectID,SubjectName")] Subject classunit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ClassUnits.Add(classunit);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(classunit);
        //}

        // GET: ClassUnits/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subject classunit = db.ClassUnits.Find(id);
        //    if (classunit == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(classunit);
        //}

        // POST: ClassUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SubjectID,SubjectName")] Subject classunit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(classunit).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(classunit);
        //}

        // GET: ClassUnits/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subject classunit = db.ClassUnits.Find(id);
        //    if (classunit == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(classunit);
        //}

        // POST: ClassUnits/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Subject classunit = db.ClassUnits.Find(id);
        //    db.ClassUnits.Remove(classunit);
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
