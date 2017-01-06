using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Awesome.Model;
using Awesome.Models;
using Awesome.Data.Contracts;
using Awesome.Data;

namespace Awesome.Controllers
{
    public class FoldersController : Controller
    {
        private IAwesomeUow Uow { get; set; }

        public FoldersController(IAwesomeUow uow)
        {
            Uow = uow;
        }

        // GET: Folders
        public ActionResult Index()
        {
            return View(Uow.Folders.GetSomeFolders(3));
        }

        // GET: Folders/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //Folder Folder = db.Folders.Find(id);
        //    if (Folder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Folder);
        //}

        // GET: Folders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Folders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FolderID,FolderName")] Folder Folder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Folders.Add(Folder);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(Folder);
        //}

        // GET: Folders/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Folder Folder = db.Folders.Find(id);
        //    if (Folder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Folder);
        //}

        // POST: Folders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "FolderID,FolderName")] Folder Folder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(Folder).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(Folder);
        //}

        // GET: Folders/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Folder Folder = db.Folders.Find(id);
        //    if (Folder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Folder);
        //}

        // POST: Folders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Folder Folder = db.Folders.Find(id);
        //    db.Folders.Remove(Folder);
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
