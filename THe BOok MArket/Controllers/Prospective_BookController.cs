using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THe_BOok_MArket.Models;

namespace THe_BOok_MArket.Controllers
{
    public class Prospective_BookController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: Prospective_Book
        public ActionResult Index()
        {
            var prospective_Book = db.Prospective_Book.Include(p => p.Book_Supplier);
            return View(prospective_Book.ToList());
        }

        // GET: Prospective_Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospective_Book prospective_Book = db.Prospective_Book.Find(id);
            if (prospective_Book == null)
            {
                return HttpNotFound();
            }
            return View(prospective_Book);
        }

        // GET: Prospective_Book/Create
        public ActionResult Create()
        {
            ViewBag.BookSupplier_ID = new SelectList(db.Book_Supplier, "BookSupplier_ID", "BookSupplier_Name");
            return View();
        }

        // POST: Prospective_Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProsBook_ID,BookSupplier_ID,ProsBook_Date")] Prospective_Book prospective_Book)
        {
            if (ModelState.IsValid)
            {
                db.Prospective_Book.Add(prospective_Book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookSupplier_ID = new SelectList(db.Book_Supplier, "BookSupplier_ID", "BookSupplier_Name", prospective_Book.BookSupplier_ID);
            return View(prospective_Book);
        }

        // GET: Prospective_Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospective_Book prospective_Book = db.Prospective_Book.Find(id);
            if (prospective_Book == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookSupplier_ID = new SelectList(db.Book_Supplier, "BookSupplier_ID", "BookSupplier_Name", prospective_Book.BookSupplier_ID);
            return View(prospective_Book);
        }

        // POST: Prospective_Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProsBook_ID,BookSupplier_ID,ProsBook_Date")] Prospective_Book prospective_Book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prospective_Book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookSupplier_ID = new SelectList(db.Book_Supplier, "BookSupplier_ID", "BookSupplier_Name", prospective_Book.BookSupplier_ID);
            return View(prospective_Book);
        }

        // GET: Prospective_Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospective_Book prospective_Book = db.Prospective_Book.Find(id);
            if (prospective_Book == null)
            {
                return HttpNotFound();
            }
            return View(prospective_Book);
        }

        // POST: Prospective_Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prospective_Book prospective_Book = db.Prospective_Book.Find(id);
            db.Prospective_Book.Remove(prospective_Book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
