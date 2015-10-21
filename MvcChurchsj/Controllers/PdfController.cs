using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcChurchsj.Models;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using PagedList;

namespace MvcChurchsj.Controllers
{
    public class PdfController : Controller
    {
        private churchdbEntities4 db = new churchdbEntities4();

        //
        // GET: /Pdf/

        public ActionResult Index(int page = 1)
        {
            churchdbEntities4 db = new churchdbEntities4();
            return View(db.Pdtables.OrderByDescending(v => v.pid).ToPagedList(page, 3));
        }
       

        //
        // GET: /Pdf/Details/5

        public ActionResult Details(int id = 0)
        {
            Pdtable pdtable = db.Pdtables.Find(id);
            if (pdtable == null)
            {
                return HttpNotFound();
            }
            return View(pdtable);
        }

        //
        // GET: /Pdf/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pdf/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pdtable pdtable, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null && file.ContentLength > 0)
            {
                churchdbEntities4 db = new churchdbEntities4();
                string PdfName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Puploads/" + PdfName);

                // save image in folder
                file.SaveAs(physicalPath);
                Pdtable newRecord = new Pdtable();
                newRecord.pname = Request.Form["pname"];
                newRecord.pdate = Request.Form["pdate"];
                newRecord.pdfdoc = "~/Puploads/" + PdfName;
                db.Pdtables.Add(newRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pdtable);
        }

        //
        // GET: /Pdf/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Pdtable pdtable = db.Pdtables.Find(id);
            if (pdtable == null)
            {
                return HttpNotFound();
            }
            return View(pdtable);
        }

        //
        // POST: /Pdf/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pdtable pdtable, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null && file.ContentLength > 0)
            {
                churchdbEntities4 db = new churchdbEntities4();
                string PdfName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Puploads/" + PdfName);

                // save image in folder
                file.SaveAs(physicalPath);
                pdtable.pname = Request.Form["pname"];
                pdtable.pdate = Request.Form["pdate"];
                pdtable.pdfdoc = "~/Puploads/" + PdfName;
                db.Entry(pdtable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(pdtable);
        }

        //
        // GET: /Pdf/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Pdtable pdtable = db.Pdtables.Find(id);
            if (pdtable == null)
            {
                return HttpNotFound();
            }
            return View(pdtable);
        }

        //
        // POST: /Pdf/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pdtable pdtable = db.Pdtables.Find(id);
            /*---------------------------------------------------------DELETE Image from folder-------------*/
            string fullPath = Request.MapPath(pdtable.pdfdoc);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            /*---------------------------------------------------------DELETE Image from folder End-------------*/
            db.Pdtables.Remove(pdtable);
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