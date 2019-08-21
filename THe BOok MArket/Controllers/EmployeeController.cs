using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THe_BOok_MArket.Models;

namespace THe_BOok_MArket.Controllers
{
    [Authorize(Roles = "800")]
    public class EmployeeController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: Employee
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Employee_Gender).Include(e => e.Employee_Title).Include(e => e.User);
            return View(employees.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.EmpGender_ID = new SelectList(db.Employee_Gender, "EmpGender_ID", "Gender_Description");
            ViewBag.EmpTitle_ID = new SelectList(db.Employee_Title, "EmpTitle_ID", "Title_Description");
             
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Employee_ID,User_ID,Employee_Name,Employee_Surname,Employee_Address,Emp_Phone,Emp_Email,ID_Number,EmpTitle_ID,EmpGender_ID,ImageData")] Employee employee)
        {


            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
                string extension = Path.GetExtension(employee.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                employee.ImageData = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                employee.ImageFile.SaveAs(fileName);


                using (The_Book_MarketEntities db = new The_Book_MarketEntities())
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                }
                ViewBag.FileStatus = "Employee Added successfully.";


                return RedirectToAction("Index");
            }
            ViewBag.EmpGender_ID = new SelectList(db.Employee_Gender, "EmpGender_ID", "Gender_Description", employee.EmpGender_ID);
            ViewBag.EmpTitle_ID = new SelectList(db.Employee_Title, "EmpTitle_ID", "Title_Description", employee.EmpTitle_ID);
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "UserName", employee.User_ID);
            return View(employee);
        }
  

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpGender_ID = new SelectList(db.Employee_Gender, "EmpGender_ID", "Gender_Description", employee.EmpGender_ID);
            ViewBag.EmpTitle_ID = new SelectList(db.Employee_Title, "EmpTitle_ID", "Title_Description", employee.EmpTitle_ID);
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "UserName", employee.User_ID);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Employee_ID,User_ID,Employee_Name,Employee_Surname,Employee_Address,Emp_Phone,Emp_Email,ID_Number,EmpTitle_ID,EmpGender_ID,ImageData")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpGender_ID = new SelectList(db.Employee_Gender, "EmpGender_ID", "Gender_Description", employee.EmpGender_ID);
            ViewBag.EmpTitle_ID = new SelectList(db.Employee_Title, "EmpTitle_ID", "Title_Description", employee.EmpTitle_ID);
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "UserName", employee.User_ID);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
