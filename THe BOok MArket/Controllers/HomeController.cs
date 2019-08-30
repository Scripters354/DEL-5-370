using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THe_BOok_MArket.Models;
using Microsoft.Reporting.WebForms;

namespace THe_BOok_MArket.Controllers
{
    public class HomeController : Controller
    {

        The_Book_MarketEntities db = new The_Book_MarketEntities();

      [Authorize]
      //Report Views
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InventoryList()
        {
            return View(db.Inventories.ToList());
        }

        public ActionResult SalesList()
        {
            return View(db.Sales.ToList());
        }
        public ActionResult OrderList()
        {
            return View(db.Inventory_Supplier_Order.ToList());
        }
        public ActionResult SuppOrderList()
        {
            return View(db.Order_Line.ToList());
        }

        public ActionResult InventoryTypeList()
        {
            return View(db.Inventory_Type.ToList());
        }
        public ActionResult StockTurnsList()
        {
            return View(db.Stock_Turns.ToList());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult InvReports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/Inventory.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "InventoryDataSet";
            reportDataSource.Value = db.Inventories.ToList();
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
            Response.AddHeader("content-disposition", "attachment;filename = Inventory_Report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);


        }

        public ActionResult OrdReports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/Order.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "OrderDataSet";
            reportDataSource.Value = db.Order_Line.ToList();
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
            Response.AddHeader("content-disposition", "attachment;filename = Supplier_Order_Report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);


        }
        public ActionResult SaleReports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/Sale.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "SaleSet";
            reportDataSource.Value = db.Sales.ToList();
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
            Response.AddHeader("content-disposition", "attachment;filename = Sale_Report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);


        }
        public ActionResult STReports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/StockTurns.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "StockTurnsSet";
            reportDataSource.Value = db.Stock_Turns.ToList();
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
            Response.AddHeader("content-disposition", "attachment;filename = Stock_Turns_Report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);


        }
        public ActionResult SupReports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/Supplier.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "SupplierDataSet";
            reportDataSource.Value = db.Inventory_Supplier.ToList();
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
            Response.AddHeader("content-disposition", "attachment;filename = Supplier_Report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);


        }
        public ActionResult GetData()
        {
            int book = db.Inventory_Type.Where(x => x.InventoryType_Name == "Book").Count();
            int stationery = db.Inventory_Type.Where(x => x.InventoryType_Name == "Stationery").Count();
            int accessories = db.Inventory_Type.Where(x => x.InventoryType_Name == "Accessories").Count();
            //int tap_to_pay = context.Inventory_Type.Where(x => x.InventoryType_Name == "Tap-to-pay").Count();
            Ratio obj = new Ratio();
            obj.Book = book;
            obj.Stationery = stationery;
            obj.Accessories = accessories;
            //obj.Tap_to_pay = tap_to_pay;



            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public Ratio PiechartValues()
        {

            int book = db.Inventory_Type.Where(x => x.InventoryType_Name == "Book").Count();
            int stationery = db.Inventory_Type.Where(x => x.InventoryType_Name == "Stationery").Count();
            int accessories = db.Inventory_Type.Where(x => x.InventoryType_Name == "Accessories").Count();
            //int tap_to_pay = context.Inventory_Type.Where(x => x.InventoryType_Name == "Tap-to-pay").Count();
            Ratio obj = new Ratio();
            obj.Book = book;
            obj.Stationery = stationery;
            obj.Accessories = accessories;

            return obj;
        }

        public class Ratio
        {
            public int Book { get; set; }
            public int Stationery { get; set; }
            public int Accessories { get; set; }
            //public int Tap_to_pay { get; set; }
        }
    }
}