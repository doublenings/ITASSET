using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_ASSET.Models;
using PagedList.Mvc;
using PagedList;
using System.Net.Mail;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;

namespace IT_ASSET.Controllers
{
    public class IncStatusController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        ViewModel viewModel = new ViewModel();
        string connectionString = "Data Source= COMP05\\SQLEXPRESS;Initial Catalog=IT_ASSET_SERVER;Integrated Security=True";

        // GET: IncStatus
        public ActionResult Index(int? i,string search)
        {
            
            var status = db.tbl_incident_case.Include(c => c.INC_STATUS);
           
            return View(db.View_inc_status.OrderByDescending(s => s.INC_CODE).Where(s => s.INC_TOPIC.Contains(search) || s.INC_CODE.Contains(search) || s.INC_REQUESTER.Contains(search)|| search == null).ToList().ToPagedList(i ?? 1, 12));
        }

        // GET: IncStatus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_inc_status view_inc_status = db.View_inc_status.Find(id);
            if (view_inc_status == null)
            {
                return HttpNotFound();
            }
            return View(view_inc_status);
        }

        // GET: IncStatus/Create
        public ActionResult Create()
        {
            tbl_incident_case tbl_Incident_Case = new tbl_incident_case();
            ViewData["INC_STATUS"] = new SelectList(db.tbl_Incident_status, "INC_STATUS", "STA_DESCRIPTION", tbl_Incident_Case.INC_STATUS);
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(incident_case tbl_Incident_Case)
        {
            if (ModelState.IsValid)
            {  
                using (SqlConnection con = new SqlConnection(connectionString))
            {
                    SqlCommand cmd = new SqlCommand("AddIncident", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@INC_DATE", tbl_Incident_Case.INC_DATE);
                    cmd.Parameters.AddWithValue("@INC_REQUESTER", tbl_Incident_Case.INC_REQUESTER);
                    cmd.Parameters.AddWithValue("@INC_TOPIC", tbl_Incident_Case.INC_TOPIC);
                    cmd.Parameters.AddWithValue("@INC_STATUS", tbl_Incident_Case.INC_STATUS);
                    cmd.Parameters.AddWithValue("@INC_DESCRIPTION", tbl_Incident_Case.INC_DESCRIPTION);

                   
                    cmd.Parameters.Add("@INC_CODE", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@INC_CODE"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewBag.Message = "Success";

                    ViewBag.EmpCount = cmd.Parameters["@INC_CODE"].Value.ToString();


                   
                var email = Session["USER_EMAIL"].ToString();
                MailMessage mm = new MailMessage();
                mm.To.Add("itservice@pranda.co.th");
                mm.From = new MailAddress(email);
                mm.Subject = "แจ้งปัญหารับแจ้ง";
                   
                mm.IsBodyHtml = true;
                mm.Body = GetFormattedMessageHTML();
                

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail01.pranda.co.th";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential();

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                   System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                   System.Security.Cryptography.X509Certificates.X509Chain chain,
                   System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                smtp.Send(mm);
                   
                return RedirectToAction("Index");
            }
            }
           
               
           
            return View(tbl_Incident_Case);
        }

        private String GetFormattedMessageHTML()
        {
            return "<b> เรียน IT SUPPORT </b>" + "</br>"  +
                        "แจ้งปัญหาของ : " + Session["USER_NAME"].ToString()  + " <br /> " +
                        "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +
                        "เลขที่ใบแจ้ง :  " + ViewBag.EmpCount + " <br /> " +
                        "สามารถติดต่อสอบถามปัญหาเพิ่มเติม    ติดต่อเบอร์โทร" + " "+ Session["USER_EXTENSION"].ToString() + "<br />" +
                        "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>"; 
                   
            
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
