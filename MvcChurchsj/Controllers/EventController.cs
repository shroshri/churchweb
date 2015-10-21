using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcChurchsj.Models;
using System.Data.SqlClient;
using System.Configuration;
using PagedList;


namespace MvcChurchsj.Controllers
{
    public class EventController : Controller
    {
        private churchdbEntities2 db = new churchdbEntities2();

        //
        // GET: /Event/

        public ActionResult Index(int page = 1)
        {
            churchdbEntities2 db = new churchdbEntities2();
            return View(db.Evtables.OrderByDescending(v => v.eid).ToPagedList(page, 3));
        }

        public ActionResult Elisting(int page = 1)
        {
            churchdbEntities2 db = new churchdbEntities2();
            return View(db.Evtables.OrderByDescending(v => v.eid).ToPagedList(page, 3));


            //return View(db.Etables.ToList());
        }


        public ActionResult Edetail(int id = 0)
        {
            Evtable evtable = db.Evtables.Find(id);
            if (evtable == null)
            {
                return HttpNotFound();
            }
            return View(evtable);
        }

        //
        // GET: /Event/Details/5

        public ActionResult Details(int id = 0)
        {
            Evtable evtable = db.Evtables.Find(id);
            if (evtable == null)
            {
                return HttpNotFound();
            }
            return View(evtable);
        }

        //
        // GET: /Event/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Event/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Evtable evtable)
        {
            if (ModelState.IsValid)
            {
                db.Evtables.Add(evtable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evtable);
        }

        //
        // GET: /Event/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Evtable evtable = db.Evtables.Find(id);
            if (evtable == null)
            {
                return HttpNotFound();
            }
            return View(evtable);
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Evtable evtable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evtable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evtable);
        }

        //
        // GET: /Event/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Evtable evtable = db.Evtables.Find(id);
            if (evtable == null)
            {
                return HttpNotFound();
            }
            return View(evtable);
        }

        //
        // POST: /Event/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evtable evtable = db.Evtables.Find(id);
            db.Evtables.Remove(evtable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}