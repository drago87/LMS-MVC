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
    public class LessonsController : Controller
    {
        public readonly IRepository<Lesson> _lessonRepo;
        public readonly IUnitOfWork _uow;

        public LessonsController(IRepository<Lesson> lessonRepository, IUnitOfWork uow)
        {
            _lessonRepo = lessonRepository;
            _uow = uow;
        }

        // GET: Lessons
        public ActionResult Index()
        {
            var lessons = _uow.Lessons.GetAll();
            return View(lessons);
        }

        // GET: Lessons/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //Lesson lesson = db.Lessons.Find(id);
        //    if (lesson == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lesson);
        //}

        // GET: Lessons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "LessonID,LessonName")] Lesson lesson)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Lessons.Add(lesson);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(lesson);
        //}

        // GET: Lessons/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Lesson lesson = db.Lessons.Find(id);
        //    if (lesson == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lesson);
        //}

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "LessonID,LessonName")] Lesson lesson)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(lesson).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(lesson);
        //}

        // GET: Lessons/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Lesson lesson = db.Lessons.Find(id);
        //    if (lesson == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lesson);
        //}

        // POST: Lessons/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Lesson lesson = db.Lessons.Find(id);
        //    db.Lessons.Remove(lesson);
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
