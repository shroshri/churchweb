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
    public class NewsController : Controller
    {
        private churchdbEntities1 db = new churchdbEntities1();

        //
        // GET: /News/

        public ActionResult Index(int page = 1)
        {
            churchdbEntities1 db = new churchdbEntities1();
            return View(db.Newstables.OrderByDescending(v => v.Nid).ToPagedList(page, 3));
        }
        /*---------------------------------------------------------*/
        public ActionResult Nlisting(int page = 1)
        {
            churchdbEntities1 db = new churchdbEntities1();
            return View(db.Newstables.OrderByDescending(v => v.Nid).ToPagedList(page, 3));
        }


        public ActionResult Ndetail(int id = 0)
        {
            Newstable newstable = db.Newstables.Find(id);
            if (newstable == null)
            {
                return HttpNotFound();
            }
            return View(newstable);
        }
        //
        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            Newstable newstable = db.Newstables.Find(id);
            if (newstable == null)
            {
                return HttpNotFound();
            }
            return View(newstable);
        }

        //
        // GET: /News/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Newstable newstable, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null && file.ContentLength > 0)
            {
                churchdbEntities1 db = new churchdbEntities1();
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/nimage/" + ImageName);

                // save image in folder
                file.SaveAs(physicalPath);


                Newstable newRecord = new Newstable();

                newRecord.Nhead= Request.Form["Nhead"];
                newRecord.Ndate = Request.Form["Ndate"];
                newRecord.Ntext = Request.Form["Ntext"];
                newRecord.NUrl = "~/nimage/" + ImageName;
                db.Newstables.Add(newRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(newstable);
        }

        //
        // GET: /News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Newstable newstable = db.Newstables.Find(id);
            if (newstable == null)
            {
                return HttpNotFound();
            }
            return View(newstable);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Newstable newstable, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null && file.ContentLength > 0)
            {
                churchdbEntities1 db = new churchdbEntities1();
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/nimage/" + ImageName);

                // save image in folder
                file.SaveAs(physicalPath);

                newstable.Nhead = Request.Form["Nhead"];
                newstable.Ndate = Request.Form["Ndate"];
                newstable.Ntext = Request.Form["Ntext"];
                newstable.NUrl = "~/nimage/" + ImageName;
                db.Entry(newstable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(newstable);
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Newstable newstable = db.Newstables.Find(id);
            if (newstable == null)
            {
                return HttpNotFound();
            }
            return View(newstable);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Newstable newstable = db.Newstables.Find(id);
            /*---------------------------------------------------------DELETE Image from folder-------------*/
            string fullPath = Request.MapPath(newstable.NUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.Newstables.Remove(newstable);
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