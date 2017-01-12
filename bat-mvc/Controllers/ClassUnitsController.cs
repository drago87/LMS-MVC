using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace bat_mvc.Controllers
{
    [Authorize(Roles="Teacher")]
    public class ClassUnitsController : Controller
    {
        public readonly IRepository<ClassUnit> _class;
        public readonly IUnitOfWork _uow;

        public ApplicationDbContext _ctx = new ApplicationDbContext();

        public ClassUnitsController(IRepository<ClassUnit> classunitRepository, IUnitOfWork uow)
        {
            _class = classunitRepository;
            _uow = uow;
        }

        // GET: ClassUnits
        public ActionResult Index()
        {
            //var classunits = _uow.Classunits.GetAll();
            var classunits = _ctx.Classunits.ToList();
            ViewBag.Folder1Message = "temp1";
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

                addFolder(classunit.ClassName + "Shared");
                
                addFolder(classunit.ClassName + "Submission");
                
                List<Folder> ClassFolders = new List<Folder>();
                ClassFolders.Add(_ctx.Folders.Single(x => x.FolderName == classunit.ClassName + "Shared"));
                ClassFolders.Add(_ctx.Folders.Single(x => x.FolderName == classunit.ClassName + "Submission"));


                _ctx.Classunits.Add(

                    new ClassUnit()
                    {
                        ClassName = classunit.ClassName,
                        Folders = ClassFolders
                    });

                _ctx.SaveChanges();

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
            ClassUnit classunit = _ctx.Classunits.SingleOrDefault(c => c.ClassUnitID == id);//_uow.Classunits.Get(id);
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
            ClassUnit classunit = _ctx.Classunits.Where(c => c.ClassUnitID == id).Include(x => x.Folders).First();
            
            //var temp = _ctx.Folders.Where(x => x.)
            //_uow.Classunits.Remove(classunit);

            //List<Folder> temp = new List<Folder>();
            var temp2 = classunit.Folders[0];
            var temp3 = classunit.Folders[1];

            _ctx.Folders.Remove(temp2);
            _ctx.Folders.Remove(temp3);
            
            
            
            _ctx.Classunits.Remove(classunit);
            _ctx.SaveChanges();
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

        public void addFolder(string name)
        {
            _ctx.Folders.Add(new Folder { FolderName = name });
            
            _ctx.SaveChanges();
           
        }
    }
}
