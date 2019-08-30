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
    public class SalesController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Customer).Include(s => s.Employee).Include(s => s.Payment_Type).Include(s => s.TaxRate);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_Name");
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name");
            ViewBag.PaymentType_ID = new SelectList(db.Payment_Type, "PaymentType_ID", "PaymentType_Name");
            ViewBag.TaxRate_ID = new SelectList(db.TaxRates, "TaxRate_ID", "Tax_Description");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sale_ID,Customer_ID,PaymentType_ID,Employee_ID,TaxRate_ID")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_Name", sale.Customer_ID);
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name", sale.Employee_ID);
            ViewBag.PaymentType_ID = new SelectList(db.Payment_Type, "PaymentType_ID", "PaymentType_Name", sale.PaymentType_ID);
            ViewBag.TaxRate_ID = new SelectList(db.TaxRates, "TaxRate_ID", "Tax_Description", sale.TaxRate_ID);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_Name", sale.Customer_ID);
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name", sale.Employee_ID);
            ViewBag.PaymentType_ID = new SelectList(db.Payment_Type, "PaymentType_ID", "PaymentType_Name", sale.PaymentType_ID);
            ViewBag.TaxRate_ID = new SelectList(db.TaxRates, "TaxRate_ID", "Tax_Description", sale.TaxRate_ID);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sale_ID,Customer_ID,PaymentType_ID,Employee_ID,TaxRate_ID")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_Name", sale.Customer_ID);
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name", sale.Employee_ID);
            ViewBag.PaymentType_ID = new SelectList(db.Payment_Type, "PaymentType_ID", "PaymentType_Name", sale.PaymentType_ID);
            ViewBag.TaxRate_ID = new SelectList(db.TaxRates, "TaxRate_ID", "Tax_Description", sale.TaxRate_ID);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
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
