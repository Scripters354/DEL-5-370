using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THe_BOok_MArket.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index(string action) //desiredAction will be stored in action
        {  
            ViewBag.Message = action;
            string path;
            switch(action) {
                case "addCustomer":
                    path = "HowtoAddCustomer.html";
                    break;
                case "searchCustomer":
                    path = "HowtoSearchCustomer.html";
                    break;
                /*More cases need to be added*/
                default:
                    path = "TheBookMarketHelp.html";
                    break;
            }
             
            return new FilePathResult(path, "text/html");
        }
    }
}