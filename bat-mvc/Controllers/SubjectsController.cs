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
//using Awesome.Model;
//using Awesome.Models;
//using Awesome.Data.Contracts;
//using Awesome.Data;

namespace bat_mvc.Controllers
{
    public class SubjectsController : Controller
    {
        public readonly IRepository<Subject> _subjectRepo;
        public readonly IUnitOfWork _uow;

        public SubjectsController(IRepository<Subject> subjectRepository, IUnitOfWork uow)
        {
            _subjectRepo = subjectRepository;
            _uow = uow;
        }

        // GET: Subjects
        public ActionResult Index()
        {
            var subjects = _uow.Subjects.GetAll();
            return View(subjects);
        }

        // GET: Subjects/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //Subject subject = db.Subjects.Find(id);
        //    if (subject == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subject);
        //}

        // GET: Subjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "SubjectID,SubjectName")] Subject subject)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Subjects.Add(subject);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(subject);
        //}

        // GET: Subjects/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subject subject = db.Subjects.Find(id);
        //    if (subject == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subject);
        //}

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SubjectID,SubjectName")] Subject subject)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(subject).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(subject);
        //}

        // GET: Subjects/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subject subject = db.Subjects.Find(id);
        //    if (subject == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subject);
        //}

        // POST: Subjects/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Subject subject = db.Subjects.Find(id);
        //    db.Subjects.Remove(subject);
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
