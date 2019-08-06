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
    public class Inventory_SupplierController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: Inventory_Supplier
        public ActionResult Index()
        {
            return View(db.Inventory_Supplier.ToList());
        }

        // GET: Inventory_Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_Supplier inventory_Supplier = db.Inventory_Supplier.Find(id);
            if (inventory_Supplier == null)
            {
                return HttpNotFound();
            }
            return View(inventory_Supplier);
        }

        // GET: Inventory_Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory_Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvSupplier_ID,InvSupp_Name,InvSupp_Address,InvSupp_Email,InvSupp_Phone")] Inventory_Supplier inventory_Supplier)
        {
            if (ModelState.IsValid)
            {
                db.Inventory_Supplier.Add(inventory_Supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventory_Supplier);
        }

        // GET: Inventory_Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_Supplier inventory_Supplier = db.Inventory_Supplier.Find(id);
            if (inventory_Supplier == null)
            {
                return HttpNotFound();
            }
            return View(inventory_Supplier);
        }

        // POST: Inventory_Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvSupplier_ID,InvSupp_Name,InvSupp_Address,InvSupp_Email,InvSupp_Phone")] Inventory_Supplier inventory_Supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory_Supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory_Supplier);
        }

        // GET: Inventory_Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_Supplier inventory_Supplier = db.Inventory_Supplier.Find(id);
            if (inventory_Supplier == null)
            {
                return HttpNotFound();
            }
            return View(inventory_Supplier);
        }

        // POST: Inventory_Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory_Supplier inventory_Supplier = db.Inventory_Supplier.Find(id);
            db.Inventory_Supplier.Remove(inventory_Supplier);
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
