using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using IT_ASSET.Models;

namespace IT_ASSET.Controllers
{
    public class ReqAFRegisterController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        string connectionString = "Data Source= COMP05\\SQLEXPRESS;Initial Catalog=IT_ASSET_SERVER;Integrated Security=True";
        // GET: ReqAFRegister
        public ActionResult Index()
        {
            return View(db.View_req_af_register.OrderByDescending(s => s.AF_CODE).ToList());
        }
     

        // GET: ReqAFRegister/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_af_follow tbl_req_af = db.View_req_af_follow.SingleOrDefault(m => m.AF_CODE == id);
            if (tbl_req_af == null)
            {
                return HttpNotFound();
            }
            return View(tbl_req_af);
        }

        // GET: ReqAFRegister/Create
        public ActionResult Create()
        {
            ViewData["ALLOW_STATUS"] = new SelectList(db.tbl_req_allow_status, "ALLOW_STATUS", "ALLOW_DESC");
            ViewData["ID_APPLICANT"] = new SelectList(db.tbl_req_applicant, "ID_APPLICANT", "APPLICANT_DESC");
            ViewData["AF_STATUS"] = new SelectList(db.tbl_req_af_status, "AF_STATUS", "AF_STATUS_NAME");
            return View();
        }

        // POST: ReqAFRegister/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( tbl_req_af tbl_req_af)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddReqAF", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Allow_STATUS", tbl_req_af.ALLOW_STATUS);
                    cmd.Parameters.AddWithValue("@ID_Applicant ", tbl_req_af.ID_APPLICANT);
                    cmd.Parameters.AddWithValue("@Mem_No", tbl_req_af.MEM_NO);
                    cmd.Parameters.AddWithValue("@AF_Site", tbl_req_af.AF_SITE);
                    cmd.Parameters.AddWithValue("@AF_Folder", tbl_req_af.AF_FOLDER);
                    cmd.Parameters.AddWithValue("@AF_Status", tbl_req_af.AF_STATUS);
                    cmd.Parameters.AddWithValue("@REQ_STATUS", tbl_req_af.REQ_STATUS);
                    cmd.Parameters.AddWithValue("@AF_Note", tbl_req_af.AF_NOTE);
                    cmd.Parameters.AddWithValue("@AF_REQUESTER", tbl_req_af.AF_REQUESTER);

                    cmd.Parameters.AddWithValue("@AF_DATE", tbl_req_af.AF_DATE);


                    cmd.Parameters.Add("@AF_CODE", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@AF_CODE"].Direction = ParameterDirection.Output;


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewBag.EmpCount = cmd.Parameters["@AF_CODE"].Value.ToString();


                    var email = Session["USER_EMAIL"].ToString();
                    MailMessage mm = new MailMessage();
                    mm.To.Add("itservice@pranda.co.th");
                    mm.From = new MailAddress(email);
                    mm.Subject = "แบบฟอร์มการขอสิทธิ ALFRESCO";

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
                    TempData["msg"] = "<script>alert('Change succesfully');</script>";
                    return RedirectToAction("Index");
 
                }
               
            }
            ViewData["ALLOW_STATUS"] = new SelectList(db.tbl_req_allow_status, "ALLOW_STATUS", "ALLOW_DESC", tbl_req_af.ALLOW_STATUS);
            ViewData["ID_APPLICANT"] = new SelectList(db.tbl_req_applicant, "ID_APPLICANT", "APPLICANT_DESC", tbl_req_af.ID_APPLICANT);
            ViewData["AF_STATUS"] = new SelectList(db.tbl_req_af_status, "AF_STATUS", "AF_STATUS_NAME",tbl_req_af.AF_STATUS);
            return View(tbl_req_af);
        }
        private string GetFormattedMessageHTML()
        {

            return "<b> เรียน IT SUPPORT </b>" + "</br>" + 
                "การขอสิทธิ ALFRESCO : " + Session["USER_NAME"].ToString() + "<br />" +
                 "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +
                 "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                 "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                 "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";

        }


        // GET: ReqAFRegister/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_req_af tbl_req_af = db.tbl_req_af.Find(id);
            if (tbl_req_af == null)
            {
                return HttpNotFound();
            }
            ViewData["ID_APPLICANT"] = new SelectList(db.tbl_req_applicant, "ID_APPLICANT", "APPLICANT_DESC");
            return View(tbl_req_af);
        }

        // POST: ReqAFRegister/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AF_ID,AF_CODE,MEM_NO,AF_DATE,ALLOW_STATUS,ID_APPLICANT,AF_SITE,AF_FOLDER,AF_STATUS,AF_NOTE,AF_REQUESTER,AF_APPROVE_OWNER,AF_APPROVE_OWNER_DATE,AF_APPROVE,AF_APPROVE_DATE,AF_OPEN_BY,AF_OPEN_DATE,AF_ASSIGN_TO,AF_ASSIGN_DATE,AF_SUBMIT_DATE,AF_CLOSE,AF_CLOSE_DATE,REQ_STATUS,AF_BILL_NO,AF_BILL_DATE,AF_COST,USER_CREATE,USER_UPDATE,CREATE_DATE,UPDATE_DATE")] tbl_req_af tbl_req_af)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_req_af).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ID_APPLICANT"] = new SelectList(db.tbl_req_applicant, "ID_APPLICANT", "APPLICANT_DESC", tbl_req_af.ID_APPLICANT);
            return View(tbl_req_af);
        }

        // GET: ReqAFRegister/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_req_af tbl_req_af = db.tbl_req_af.Find(id);
            if (tbl_req_af == null)
            {
                return HttpNotFound();
            }
            return View(tbl_req_af);
        }

        // POST: ReqAFRegister/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_req_af tbl_req_af = db.tbl_req_af.Find(id);
            db.tbl_req_af.Remove(tbl_req_af);
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
        public ActionResult FollowAF()
        {
            return View(db.View_req_af_follow.ToList());
        }
        public ActionResult Detail_AF(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_af_follow view_Req_Af_Follow= db.View_req_af_follow.Find(id);
            if (view_Req_Af_Follow == null)
            {
                return HttpNotFound();
            }
            return View(view_Req_Af_Follow);
        }
      
    }
}
