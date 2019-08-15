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
    public class UserRoleController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();

        // GET: UserRole
        public ActionResult Index()
        {
            return View(db.User_Role.ToList());
        }

        // GET: UserRole/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Role user_Role = db.User_Role.Find(id);
            if (user_Role == null)
            {
                return HttpNotFound();
            }
            return View(user_Role);
        }

        // GET: UserRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRole/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserRole_ID,UserRole_Description")] User_Role user_Role)
        {
            if (ModelState.IsValid)
            {
                db.User_Role.Add(user_Role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_Role);
        }

        // GET: UserRole/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Role user_Role = db.User_Role.Find(id);
            if (user_Role == null)
            {
                return HttpNotFound();
            }
            return View(user_Role);
        }

        // POST: UserRole/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserRole_ID,UserRole_Description")] User_Role user_Role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_Role);
        }

        // GET: UserRole/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Role user_Role = db.User_Role.Find(id);
            if (user_Role == null)
            {
                return HttpNotFound();
            }
            return View(user_Role);
        }

        // POST: UserRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_Role user_Role = db.User_Role.Find(id);
            db.User_Role.Remove(user_Role);
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
