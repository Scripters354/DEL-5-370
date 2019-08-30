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
    public class Inventory_Supplier_OrderController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: Inventory_Supplier_Order
        public ActionResult Index()
        {
            var inventory_Supplier_Order = db.Inventory_Supplier_Order.Include(i => i.Inventory_Supplier).Include(i => i.Order_Status);
            return View(inventory_Supplier_Order.ToList());
        }

        // GET: Inventory_Supplier_Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_Supplier_Order inventory_Supplier_Order = db.Inventory_Supplier_Order.Find(id);
            if (inventory_Supplier_Order == null)
            {
                return HttpNotFound();
            }
            return View(inventory_Supplier_Order);
        }

        // GET: Inventory_Supplier_Order/Create
        public ActionResult Create()
        {
            ViewBag.InvSupplier_ID = new SelectList(db.Inventory_Supplier, "InvSupplier_ID", "InvSupp_Name");
            ViewBag.SuppOrder_Status_ID = new SelectList(db.Order_Status, "SuppOrder_Status_ID", "Order_Status_Description");
            return View();
        }

        // POST: Inventory_Supplier_Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvSuppOrder_ID,InvSupplier_ID,SuppOrder_Status_ID,Order_Date")] Inventory_Supplier_Order inventory_Supplier_Order)
        {
            if (ModelState.IsValid)
            {
                db.Inventory_Supplier_Order.Add(inventory_Supplier_Order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvSupplier_ID = new SelectList(db.Inventory_Supplier, "InvSupplier_ID", "InvSupp_Name", inventory_Supplier_Order.InvSupplier_ID);
            ViewBag.SuppOrder_Status_ID = new SelectList(db.Order_Status, "SuppOrder_Status_ID", "Order_Status_Description", inventory_Supplier_Order.SuppOrder_Status_ID);
            return View(inventory_Supplier_Order);
        }

        // GET: Inventory_Supplier_Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_Supplier_Order inventory_Supplier_Order = db.Inventory_Supplier_Order.Find(id);
            if (inventory_Supplier_Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvSupplier_ID = new SelectList(db.Inventory_Supplier, "InvSupplier_ID", "InvSupp_Name", inventory_Supplier_Order.InvSupplier_ID);
            ViewBag.SuppOrder_Status_ID = new SelectList(db.Order_Status, "SuppOrder_Status_ID", "Order_Status_Description", inventory_Supplier_Order.SuppOrder_Status_ID);
            return View(inventory_Supplier_Order);
        }

        // POST: Inventory_Supplier_Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvSuppOrder_ID,InvSupplier_ID,SuppOrder_Status_ID,Order_Date")] Inventory_Supplier_Order inventory_Supplier_Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory_Supplier_Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvSupplier_ID = new SelectList(db.Inventory_Supplier, "InvSupplier_ID", "InvSupp_Name", inventory_Supplier_Order.InvSupplier_ID);
            ViewBag.SuppOrder_Status_ID = new SelectList(db.Order_Status, "SuppOrder_Status_ID", "Order_Status_Description", inventory_Supplier_Order.SuppOrder_Status_ID);
            return View(inventory_Supplier_Order);
        }

        // GET: Inventory_Supplier_Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_Supplier_Order inventory_Supplier_Order = db.Inventory_Supplier_Order.Find(id);
            if (inventory_Supplier_Order == null)
            {
                return HttpNotFound();
            }
            return View(inventory_Supplier_Order);
        }

        // POST: Inventory_Supplier_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory_Supplier_Order inventory_Supplier_Order = db.Inventory_Supplier_Order.Find(id);
            db.Inventory_Supplier_Order.Remove(inventory_Supplier_Order);
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
