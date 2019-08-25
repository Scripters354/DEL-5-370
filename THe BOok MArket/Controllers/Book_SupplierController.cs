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
    public class Book_SupplierController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: Book_Supplier
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Name")
            {
                return View(db.Book_Supplier.Where(x => x.BookSupplier_Name.Contains(search) || search == null).ToList());
            }
            else if (searchBy == "Email")
            {
                return View(db.Book_Supplier.Where(x => x.BookSupplier_Email.StartsWith(search) || search == null).ToList());
            }

            else
            {
                return View(db.Book_Supplier.Where(x => x.BookSupplier_Surname.Contains(search) || search == null).ToList());
            }


        }


        // GET: Book_Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Supplier book_Supplier = db.Book_Supplier.Find(id);
            if (book_Supplier == null)
            {
                return HttpNotFound();
            }
            return View(book_Supplier);
        }

        // GET: Book_Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book_Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookSupplier_ID,BookSupplier_Name,BookSupplier_Surname,BookSupplier_Email,BookSupplier_Address,BookSupplier_Type")] Book_Supplier book_Supplier)
        {
            if (ModelState.IsValid)
            {
                db.Book_Supplier.Add(book_Supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book_Supplier);
        }

        // GET: Book_Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Supplier book_Supplier = db.Book_Supplier.Find(id);
            if (book_Supplier == null)
            {
                return HttpNotFound();
            }
            return View(book_Supplier);
        }

        // POST: Book_Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookSupplier_ID,BookSupplier_Name,BookSupplier_Surname,BookSupplier_Email,BookSupplier_Address,BookSupplier_Type")] Book_Supplier book_Supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_Supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book_Supplier);
        }

        // GET: Book_Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Supplier book_Supplier = db.Book_Supplier.Find(id);
            if (book_Supplier == null)
            {
                return HttpNotFound();
            }
            return View(book_Supplier);
        }

        // POST: Book_Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book_Supplier book_Supplier = db.Book_Supplier.Find(id);
            db.Book_Supplier.Remove(book_Supplier);
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
