using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using THe_BOok_MArket.Models;


namespace THe_BOok_MArket.Controllers
{
    [AllowAnonymous]
    public class UsersAuthController : Controller
    {
        private The_Book_MarketEntities db = new The_Book_MarketEntities();
        [Authorize(Roles = "800")]
        [HttpGet]
        // GET: UsersAuth
        public ActionResult Register()
        {
            ViewBag.UserRole_ID = new SelectList(db.User_Role, "UserRole_ID", "UserRole_Description").ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "IsUserVerified, GUID")] User user)
        {
            
            bool Status = false;
            string message = "";
            //
            // Model Validation 
            if (ModelState.IsValid)
            {
                user.IsUserVerified = false;
                #region //User already Exist 

                var isExist = IsUserExist(user.UserName);
                if (isExist)
                {
                    ModelState.AddModelError("UserExist", "User already exist");
                    return View(user);
                }
             
                #endregion

                #region Generate user Activation Code 
                user.GUID = Guid.NewGuid();
                #endregion

                #region  Password Hashing 
                user.UserPassword = EncryptPassword.Hash(user.UserPassword);
                //user.PassConfirm = EncryptPassword.Hash(user.PassConfirm); 
                #endregion   

                #region Save user to Database
                
                using (The_Book_MarketEntities dc = new The_Book_MarketEntities())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();

                    //Send Email to User
                    SendVerificationLinkEmail(user.UserName, user.GUID.ToString());
                    message = "Registration successfully done. Account activation link " +
                        " has been sent to user email id:" + user.UserName;
                    Status = true;
                }

                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            ViewBag.UserRole_ID = new SelectList(db.User_Role, "UserRole_ID", "UserRole_Description", user.UserRole_ID);
            return View(user);
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (The_Book_MarketEntities dc = new The_Book_MarketEntities())
            {
                /*dc.Configuration.ValidateOnSaveEnabled = false;
                // to avoid 
                // Confirm password does not match issue on save changes*/
                var v = dc.Users.Where(a => a.GUID == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsUserVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (The_Book_MarketEntities dc = new The_Book_MarketEntities())
            {
                try
                {
                    var v = dc.Users.Where(a => a.UserName == login.UserName).FirstOrDefault();
                    if (v != null)
                    {
                        if (v.IsUserVerified == false)
                        {
                            ViewBag.Message = "Please verify your email first";
                            return View();
                        }

                        if (string.Compare(EncryptPassword.Hash(login.UserPassword), v.UserPassword) == 0)
                        {
                            int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                            var ticket = new FormsAuthenticationTicket(login.UserName, login.RememberMe, timeout);
                            string encrypted = FormsAuthentication.Encrypt(ticket);
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                            cookie.Expires = DateTime.Now.AddMinutes(timeout);
                            cookie.HttpOnly = true;
                            Response.Cookies.Add(cookie);


                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            message = "Invalid credential provided";
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.Message;
                }

            }
            if (message != "")
                ViewBag.Message = message;

            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "UsersAuth");
        }
        public ActionResult LogOff()
        {
            Session["User"] = null; //it's my session variable
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut(); //you write this when you use FormsAuthentication
            return RedirectToAction("Login", "UsersAuth");
        }

        //Forgot Password
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ForgotPassword(string userName)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            bool status = false;

            using (The_Book_MarketEntities dc = new The_Book_MarketEntities())
            {
                var account = dc.Users.Where(a =>a.UserName == userName).FirstOrDefault();

                try
                {
                    if (account != null)
                    {
                        //Send email for reset password
                        string resetCode = Guid.NewGuid().ToString();
                        SendResetPasswordLinkEmail(account.UserName, resetCode);
                        //SendVerificationLinkEmail(account.UserName, resetCode);
                        account.ResetCode = resetCode;
                        // added to avoid confirm password area in model
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "Reset password link has been sent to your email id.";
                    }
                   

                    else
                    {
                        message = "Account not found";
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string Message = string.Format("{0}:{1}",
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
            ViewBag.Message = message;
            return View();
            
        }


        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound("Invalid user");
            }

            using (The_Book_MarketEntities dc = new The_Book_MarketEntities())
            {

                var user = dc.Users.Where(a => a.ResetCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetPassCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound("Invalid user");

                }

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (The_Book_MarketEntities dc = new The_Book_MarketEntities())
                {
                    var user = dc.Users.Where(a => a.ResetCode == model.ResetPassCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.UserPassword = EncryptPassword.Hash(model.NewPassword);
                        user.ResetCode = "";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }


        [HttpGet]
        [NonAction]
        public bool IsUserExist(string username)
        {
            using (The_Book_MarketEntities dc = new The_Book_MarketEntities())
            {
                var v = dc.Users.Where(a => a.UserName == username).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string userName, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/UsersAuth/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("Scripters.inf370@gmail.com", "Scripters System"); //Valid Email
            var toEmail = new MailAddress(userName);
            var fromEmailPassword = "SCRIPTERs370";

            string subject = "";
            string body = "";

            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your SCripters account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }

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

        [NonAction]
        public void SendResetPasswordLinkEmail(String username, string resetcode)
        {
            try {
                var verifyurl = "/UsersAuth/ResetPassword/" + resetcode;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyurl);

                var fromEmail = new MailAddress("Scripters.inf370@gmail.com", "The Book Market");
                var toEmail = new MailAddress(username);
                var fromEmailPassword = "SCRIPTERs370";
                string subject = "Account Reset Password";
                string body = body = "Hi,<br/><br/>We got request for reset your account password. Please click on the below link to reset your password" +
                        "<br/><br/><a href=" + link + ">Reset Password link</a>";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 25,
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
         

    }
}