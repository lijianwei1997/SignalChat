using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
     
    public class ShareController:Controller
    {
        private context db = new context();
        [HttpPost]
        public ActionResult loginAction(FormCollection form)
        {
            UserLogin u = new UserLogin();
            u.loginname = Request.Form["username"].ToString().Trim();
            u.loginpwd= Request.Form["password"].ToString().Trim();


            UserLogin user = db.UserLogins.Where(a=>a.loginname==u.loginname).FirstOrDefault();
            try
            {
                if (user.loginpwd == u.loginpwd)
                {
                    Session[Common.Common.GetSessionID()] = user;
                    return RedirectToAction("Main", "Chat");
                }
                else
                {
                    return View("Login");
                }
            }
            catch
            {

                return View("Error");
            }
        }

        public ActionResult Login()
        {
            return View();
        }
    
    }


}