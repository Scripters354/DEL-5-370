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
    public class Stock_TurnsController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: Stock_Turns
        public ActionResult Index()
        {
            return View(db.Stock_Turns.ToList());
        }

        // GET: Stock_Turns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_Turns stock_Turns = db.Stock_Turns.Find(id);
            if (stock_Turns == null)
            {
                return HttpNotFound();
            }
            return View(stock_Turns);
        }

        // GET: Stock_Turns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock_Turns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockTurn_ID,Turn_Over_Ratio")] Stock_Turns stock_Turns)
        {
            if (ModelState.IsValid)
            {
                db.Stock_Turns.Add(stock_Turns);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stock_Turns);
        }

        // GET: Stock_Turns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_Turns stock_Turns = db.Stock_Turns.Find(id);
            if (stock_Turns == null)
            {
                return HttpNotFound();
            }
            return View(stock_Turns);
        }

        // POST: Stock_Turns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockTurn_ID,Turn_Over_Ratio")] Stock_Turns stock_Turns)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock_Turns).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stock_Turns);
        }

        // GET: Stock_Turns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_Turns stock_Turns = db.Stock_Turns.Find(id);
            if (stock_Turns == null)
            {
                return HttpNotFound();
            }
            return View(stock_Turns);
        }

        // POST: Stock_Turns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock_Turns stock_Turns = db.Stock_Turns.Find(id);
            db.Stock_Turns.Remove(stock_Turns);
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
