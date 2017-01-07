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
        public ActionResult Details(int id)
        {
            ClassUnit classunit = _uow.Classunits.Get(id);
            if (classunit == null)
            {
                return HttpNotFound();
            }
            return View(classunit);
        }

        // GET: ClassUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassUnitID,ClassName")] ClassUnit classunit)
        {
            if (ModelState.IsValid)
            {
                _uow.Classunits.Add(classunit);

                return RedirectToAction("Index");
            }

            return View(classunit);
        }

        // GET: ClassUnits/Edit/5
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            ClassUnit classunit = _uow.Classunits.Get(id);
            if (classunit == null)
            {
                return HttpNotFound();
            }
            return View(classunit);
        }

        // POST: ClassUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassUnitId,ClassUnitName")] ClassUnit classunit)
        {
            if (ModelState.IsValid)
            {
                //classunit.ObjectState = ObjectState.Modified;
                //_uow.update(classunit);
                //await _uow.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(classunit);
        }

        // GET: ClassUnits/Delete/5
        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            ClassUnit classunit = _uow.Classunits.Get(id);
            if (classunit == null)
            {
                return HttpNotFound();
            }
            return View(classunit);
        }

        // POST: ClassUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassUnit classunit = _uow.Classunits.Get(id);
            _uow.Classunits.Remove(classunit);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
