using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THe_BOok_MArket.Models;
using Microsoft.Reporting.WebForms;

namespace THe_BOok_MArket.Controllers
{
    public class Inventory_TypeController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: Inventory_Type
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Name")
            {
                return View(db.Inventory_Type.Where(x => x.InventoryType_Name.Contains(search) || search == null).ToList());
            }
            else if (searchBy == "Description")
            {
                return View(db.Inventory_Type.Where(x => x.InventoryType_Description.Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.Inventory_Type.ToList());
            }


        }


        //public ActionResult Index()
        //{
        //    return View(db.Inventory_Type.ToList());
        //}

        // GET: Inventory_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_Type inventory_Type = db.Inventory_Type.Find(id);
            if (inventory_Type == null)
            {
                return HttpNotFound();
            }
            return View(inventory_Type);
        }

        // GET: Inventory_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult InventoryTypeList()
        {
            return View(/*db.Inventory_Type.ToList()*/);
        }

        public ActionResult Reports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/InventoryTypeReport.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "InventoryTypeDataSet";
            reportDataSource.Value = db.Inventory_Type.ToList();
            localreport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            else if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }

            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }

            else if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }

            else
            {
                fileNameExtension = "jpg";
            }

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localreport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename = Inventory_Type_Report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);


        }



        //public ActionResult InventoryList()
        //{
        //    var inventoryList = db.Inventories.ToList();
        //    return View(inventoryList);
        //}

        // POST: Inventory_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InventoryType_ID,InventoryType_Name,InventoryType_Description")] Inventory_Type inventory_Type)
        {
            if (ModelState.IsValid)
            {
                db.Inventory_Type.Add(inventory_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventory_Type);
        }

        // GET: Inventory_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_Type inventory_Type = db.Inventory_Type.Find(id);
            if (inventory_Type == null)
            {
                return HttpNotFound();
            }
            return View(inventory_Type);
        }

        // POST: Inventory_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InventoryType_ID,InventoryType_Name,InventoryType_Description")] Inventory_Type inventory_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory_Type);
        }

        // GET: Inventory_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_Type inventory_Type = db.Inventory_Type.Find(id);
            if (inventory_Type == null)
            {
                return HttpNotFound();
            }
            return View(inventory_Type);
        }

        // POST: Inventory_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory_Type inventory_Type = db.Inventory_Type.Find(id);
            db.Inventory_Type.Remove(inventory_Type);
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
