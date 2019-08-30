using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THe_BOok_MArket.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace THe_BOok_MArket.Controllers
{
    public class InventoriesController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: Inventories

        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Name")
            {
                return View(db.Inventories.Where(x => x.Inventory_Type_ID.Contains(search) || search == null).ToList());
            }
            else if (searchBy == "Description")
            {
                return View(db.Inventories.Where(x => x.Inventory_Description.Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.Inventories.ToList());
            }


        }
        //public ActionResult Index()
        //{
        //    var inventories = db.Inventories.Include(i => i.Inventory_Type).Include(i => i.Stock_Turns);
        //    return View(inventories.ToList());
        //}

        // GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventories/Create
        public ActionResult Create()
        {
            ViewBag.InventoryType_ID = new SelectList(db.Inventory_Type, "InventoryType_ID", "InventoryType_Name");
            ViewBag.StockTurn_ID = new SelectList(db.Stock_Turns, "StockTurn_ID", "Turn_Over_Ratio");
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Inventory_ID,InventoryType_ID,Inventory_Name,Inventory_Description,Inventory_Quantity,Minimum_Quantity,StockTurn_ID")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Inventories.Add(inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InventoryType_ID = new SelectList(db.Inventory_Type, "InventoryType_ID", "InventoryType_Name", inventory.InventoryType_ID);
            ViewBag.StockTurn_ID = new SelectList(db.Stock_Turns, "StockTurn_ID", "Turn_Over_Ratio", inventory.StockTurn_ID);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.InventoryType_ID = new SelectList(db.Inventory_Type, "InventoryType_ID", "InventoryType_Name", inventory.InventoryType_ID);
            ViewBag.StockTurn_ID = new SelectList(db.Stock_Turns, "StockTurn_ID", "Turn_Over_Ratio", inventory.StockTurn_ID);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Inventory_ID,InventoryType_ID,Inventory_Name,Inventory_Description,Inventory_Quantity,Minimum_Quantity,StockTurn_ID")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InventoryType_ID = new SelectList(db.Inventory_Type, "InventoryType_ID", "InventoryType_Name", inventory.InventoryType_ID);
            ViewBag.StockTurn_ID = new SelectList(db.Stock_Turns, "StockTurn_ID", "Turn_Over_Ratio", inventory.StockTurn_ID);
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
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

        public ActionResult InventoryList()
        {
            var inventoryList = db.Inventories.ToList();
            return View(inventoryList);
        }
        public ActionResult ExInvReport()
        {
            List<Inventory> allInventory = new List<Inventory>();
            allInventory = db.Inventories.ToList();

            ReportDocument ad = new ReportDocument();
            ad.Load(Path.Combine(Server.MapPath("~/InvReport"), "InvCrysRep.rpt"));

            ad.SetDataSource(db.Inventories.Select(c => new { Inventory_ID = c.Inventory_ID }).ToList());
            //ad.SetDataSource(db.Inventories.Select(c => new { c.Inventory_Name }).ToList());
            //rd.SetDataSource(db.Customers.Select(c => new { c.Customer_Surname }).ToList());
            //rd.SetDataSource(db.Customers.Select(c => new { c.Customer_Email }).ToList());
            //rd.SetDataSource(db.Customers.Select(c => new { c.Customer_Contact.Value }).ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = ad.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "InventoryList.pdf");

            }
            catch
            {
                throw;
            }

        }
    }
}
