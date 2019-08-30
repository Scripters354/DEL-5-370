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
    public class Order_LineController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: Order_Line
        public ActionResult Index()
        {
            var order_Line = db.Order_Line.Include(o => o.Inventory).Include(o => o.Inventory_Supplier_Order).Include(o => o.Order_Status);
            return View(order_Line.ToList());
        }

        // GET: Order_Line/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Line order_Line = db.Order_Line.Find(id);
            if (order_Line == null)
            {
                return HttpNotFound();
            }
            return View(order_Line);
        }

        // GET: Order_Line/Create
        public ActionResult Create()
        {
            ViewBag.Inventory_ID = new SelectList(db.Inventories, "Inventory_ID", "Inventory_Name");
            ViewBag.InvSuppOrder_ID = new SelectList(db.Inventory_Supplier_Order, "InvSuppOrder_ID", "InvSuppOrder_ID");
            ViewBag.SuppOrder_Status_ID = new SelectList(db.Order_Status, "SuppOrder_Status_ID", "Order_Status_Description");
            return View();
        }

        // POST: Order_Line/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvSuppOrder_ID,Inventory_ID,SuppOrder_Status_ID,Quanity,Line_Total,Date")] Order_Line order_Line)
        {
            if (ModelState.IsValid)
            {
                db.Order_Line.Add(order_Line);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Inventory_ID = new SelectList(db.Inventories, "Inventory_ID", "Inventory_Name", order_Line.Inventory_ID);
            ViewBag.InvSuppOrder_ID = new SelectList(db.Inventory_Supplier_Order, "InvSuppOrder_ID", "InvSuppOrder_ID", order_Line.InvSuppOrder_ID);
            ViewBag.SuppOrder_Status_ID = new SelectList(db.Order_Status, "SuppOrder_Status_ID", "Order_Status_Description", order_Line.SuppOrder_Status_ID);
            return View(order_Line);
        }

        // GET: Order_Line/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Line order_Line = db.Order_Line.Find(id);
            if (order_Line == null)
            {
                return HttpNotFound();
            }
            ViewBag.Inventory_ID = new SelectList(db.Inventories, "Inventory_ID", "Inventory_Name", order_Line.Inventory_ID);
            ViewBag.InvSuppOrder_ID = new SelectList(db.Inventory_Supplier_Order, "InvSuppOrder_ID", "InvSuppOrder_ID", order_Line.InvSuppOrder_ID);
            ViewBag.SuppOrder_Status_ID = new SelectList(db.Order_Status, "SuppOrder_Status_ID", "Order_Status_Description", order_Line.SuppOrder_Status_ID);
            return View(order_Line);
        }

        // POST: Order_Line/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvSuppOrder_ID,Inventory_ID,SuppOrder_Status_ID,Quanity,Line_Total,Date")] Order_Line order_Line)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Line).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Inventory_ID = new SelectList(db.Inventories, "Inventory_ID", "Inventory_Name", order_Line.Inventory_ID);
            ViewBag.InvSuppOrder_ID = new SelectList(db.Inventory_Supplier_Order, "InvSuppOrder_ID", "InvSuppOrder_ID", order_Line.InvSuppOrder_ID);
            ViewBag.SuppOrder_Status_ID = new SelectList(db.Order_Status, "SuppOrder_Status_ID", "Order_Status_Description", order_Line.SuppOrder_Status_ID);
            return View(order_Line);
        }

        // GET: Order_Line/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Line order_Line = db.Order_Line.Find(id);
            if (order_Line == null)
            {
                return HttpNotFound();
            }
            return View(order_Line);
        }

        // POST: Order_Line/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Line order_Line = db.Order_Line.Find(id);
            db.Order_Line.Remove(order_Line);
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
