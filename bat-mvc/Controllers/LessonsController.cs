using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Core.Repositories;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace bat_mvc.Controllers
{
    [Authorize]
    public class LessonsController : Controller
    {
        public readonly IRepository<Lesson> _lessonRepo;
        public readonly IUnitOfWork _uow;

        public ApplicationDbContext _ctx = new ApplicationDbContext();
        public LessonsController(IRepository<Lesson> lessonRepository, IUnitOfWork uow)
        {
            _lessonRepo = lessonRepository;
            _uow = uow;
        }

        // GET: Lessons
        public ActionResult Index()
        {
            var lessons = _ctx.Lessons.Include(x => x.ClassUnit).Include(z => z.Subject);

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
            ViewBag.Subjects = _ctx.Subjects;
            ViewBag.Classunit = _ctx.Classunits;

            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonID,LessonName")] Lesson lesson, int SubjectID, int ClassUnitID, DateTime StartTime,DateTime StopTime)
        {
            if (ModelState.IsValid)
            {
                lesson.StartTime = StartTime;
                lesson.StopTime = StopTime;
                lesson.Subject = _ctx.Subjects.SingleOrDefault(x => x.SubjectID == SubjectID);
                lesson.ClassUnit = _ctx.Classunits.SingleOrDefault(x => x.ClassUnitID == ClassUnitID);
                
                _ctx.Lessons.Add(lesson);
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lesson);
        }

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
