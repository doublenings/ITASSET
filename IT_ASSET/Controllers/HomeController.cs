using IT_ASSET.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IT_ASSET.Controllers
{
    public class HomeController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        //string connectionString = "Data Source= COMP05\\SQLEXPRESS;Initial Catalog=IT_ASSET_SERVER;Integrated Security=True";
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(tbl_user tbl_User)
        {
            if (ModelState.IsValid)
            {
                using (IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities())
                {
                    
                   
                    var obj = db.tbl_user.Where(User => User.USER_NO.Equals(tbl_User.USER_NO) && User.USER_PASSWORD.Equals(tbl_User.USER_PASSWORD)).FirstOrDefault();

                    if (obj != null)
                    {

                        Session["USER_ID"] = obj.USER_ID.ToString();
                        Session["USER_NO"] = obj.USER_NO.ToString();
                        Session["USER_NAME"] = obj.USER_NAME.ToString();
                        Session["USER_EMAIL"] = obj.USER_EMAIL.ToString();
                        Session["USER_EXTENSION"] = obj.USER_EXTENSION.ToString();
                        Session["USER_ROLE"] = obj.USER_ROLE.ToString();
                        Session["USER_EMAIL_APPROVE"] = obj.USER_EMAIL_APPROVE.ToString();


                        return RedirectToAction("Index", "IncStatus");
                    }
                    else
                    {
                        
                        ViewData["message"] = "Login Failed !";

                    }



                }


            }
            return View(tbl_User);
        }
        public ActionResult Logout()
        {
            if (Session["USER_NO"] != null)
            {
                Session.Abandon();
            }

            return RedirectToAction("Login");
            // ViewBag.Message = "Your application description page.";

            // return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }

    }
}