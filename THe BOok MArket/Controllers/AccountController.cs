using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using THe_BOok_MArket.Models;

namespace THe_BOok_MArket.Controllers
{
    public class AccountController : Controller
    {
        private The_Book_MarketEntities1 db = new The_Book_MarketEntities1();

        // GET: Account
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.User_Role);
            return View(users.ToList());
        }

        // GET: Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            ViewBag.UserRole_ID = new SelectList(db.User_Role, "UserRole_ID", "UserRole_Description");
            return View();
        }

        // POST: Account/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "UserRole_ID,GUID,IsUserVerified")] User user)
        {
            bool Status = false;
            string Message = "";
            try {

                if (ModelState.IsValid)
                {

                    var IsExist = IsEmailExists(user.UserName);
                    if (IsExist)
                    {
                        ModelState.AddModelError("EmailExist", "Email already exists");
                        return View(user);
                    }
                    // Generate activation code for employee
                    user.GUID = new Guid();

                    //Hash passwords
                    //user.ConfirmPassword = user.UserPassword;
                    //user.ConfirmPassword = EncryptPassword.Hash(user.ConfirmPassword);
                    user.UserPassword = EncryptPassword.Hash(user.UserPassword);
                    user.IsUserVerified = false;

                    //Save information to database
                    using (The_Book_MarketEntities1 db = new The_Book_MarketEntities1())
                    {
                        //db.Configuration.ValidateOnSaveEnabled = false;
                        db.Users.Add(user);
                        db.SaveChanges();

                        sendVerificationLinkEmail(user.UserName, user.GUID.ToString());
                        Message = "Account Registration successfull. Account activation link" +
                            "has been sent to your email address:" + user.UserName;
                        return RedirectToAction("Index");
                    }

                }
                ViewBag.UserRole_ID = new SelectList(db.User_Role, "UserRole_ID", "UserRole_Description", user.UserRole_ID);
                return View(user);
              

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }
            


        [NonAction]
        public bool IsEmailExists(string username)
        {
            using (The_Book_MarketEntities1 db = new The_Book_MarketEntities1())
            {
                var user = db.Users.Where(a => a.UserName == username).FirstOrDefault();

                return user == null ? false : true;
            }
        }

        [NonAction]
        public void sendVerificationLinkEmail(string username, string activationcode)
        {
           
            var verifyurl = "User/VerifyAccount/" + activationcode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyurl);

            var fromEmail = new MailAddress("scripters.inf370@gmail.com", "The Book Market");
            var toEmail = new MailAddress(username);
            var fromEmailPassword = "SCRIPTERs370";
            string subject = "Account successfully created";
            string body = "<br/><br/> We are delighted to inform you that your" +
                "profile has been created successfully. Please click on the link below to verify" +
                " your account" + "<br/><br/> <a href'" + link + "'>" + link + "<a/>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            smtp.Send(message);
        }


        // GET: Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserRole_ID = new SelectList(db.User_Role, "UserRole_ID", "UserRole_Description", user.UserRole_ID);
            return View(user);
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_ID,UserRole_ID,UserName,UserPassword,GUID,GUIDExpiry")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserRole_ID = new SelectList(db.User_Role, "UserRole_ID", "UserRole_Description", user.UserRole_ID);
            return View(user);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
