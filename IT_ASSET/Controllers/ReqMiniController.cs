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
using PagedList;
using Rotativa;

namespace IT_ASSET.Controllers
{
    public class ReqMiniController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        string connectionString = "Data Source= serverjob;Initial Catalog=IT_ASSET_SERVER;Integrated Security=True";

        // GET: ReqMini
        public ActionResult Index(int? i, string search)
        {
           
            return View(db.View_req_mini.OrderByDescending(s => s.MINI_DATE).Where(s => s.MINI_CODE.Contains(search) || s.USER_NAME.Contains(search) || s.USER_NO.Contains(search)|| s.MINI_REQUESTER.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 15));
        }
        public ActionResult IndexMini(int? i, string search)
        {
            return View(db.View_req_mini_follow.OrderByDescending(s => s.MINI_APPROVE_DATE).Where(s => s.MINI_CODE.Contains(search) || s.USER_NAME.Contains(search) || s.USER_NO.Contains(search) || s.MINI_REQUESTER.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 15));
        }
        // GET: ReqMini/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_mini tbl_req_mini = db.View_req_mini.Find(id);
            if (tbl_req_mini == null)
            {
                return HttpNotFound();
            }
            return View(tbl_req_mini);
        }
        public ActionResult DetailsMini(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_mini_follow tbl_req_mini = db.View_req_mini_follow.Find(id);
            if (tbl_req_mini == null)
            {
                return HttpNotFound();
            }
            return View(tbl_req_mini);
        }

        // GET: ReqMini/Create
        public ActionResult Create()
        {
            ViewData["ALLOW_STATUS"] = new SelectList(db.tbl_req_allow_status, "ALLOW_STATUS", "ALLOW_DESC");
            return View();
        }

        // POST: ReqMini/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_req_mini tbl_req_mini)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddReqMini", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USER_NO", tbl_req_mini.USER_NO);
                    cmd.Parameters.AddWithValue("@MINI_DATE", tbl_req_mini.MINI_DATE);
                    cmd.Parameters.AddWithValue("@Allow_STATUS", tbl_req_mini.ALLOW_STATUS);
                    cmd.Parameters.AddWithValue("@MINI_SAMPLE", tbl_req_mini.MINI_SAMPLE);
                    cmd.Parameters.AddWithValue("@MINI_PROTO", tbl_req_mini.MINI_PROTO);
                    cmd.Parameters.AddWithValue("@MINI_INFORM", tbl_req_mini.MINI_INFORM);
                   cmd.Parameters.AddWithValue("@MINI_CHM", tbl_req_mini.MINI_CHM);
                    cmd.Parameters.AddWithValue("@REQ_STATUS", tbl_req_mini.REQ_STATUS);
                    cmd.Parameters.AddWithValue("@MINI_REQUESTER", tbl_req_mini.MINI_REQUESTER);
                   

                    cmd.Parameters.Add("@MINI_CODE", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@MINI_CODE"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    ViewBag.EmpCount = cmd.Parameters["@MINI_CODE"].Value.ToString();
                    ViewBag.Requester = cmd.Parameters["@USER_NO"].Value.ToString();
                    ViewBag.Date = cmd.Parameters["@MINI_DATE"].Value.ToString();

                    

                    var email = Session["USER_EMAIL"].ToString();
                    var approve = Session["USER_EMAIL_APPROVE"].ToString();
                    MailMessage mm = new MailMessage();
                    mm.To.Add(approve);
                    mm.To.Add("ITservice@pranda.co.th");
                    mm.From = new MailAddress(email);
                    mm.Subject = "แบบฟอร์มการขอและยกเลิกการใช้เครื่อง PDJMINI02";

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
                }

                return RedirectToAction("Index");
            }
            ViewData["ALLOW_STATUS"] = new SelectList(db.tbl_req_allow_status, "ALLOW_STATUS", "ALLOW_DESC", tbl_req_mini.ALLOW_STATUS);



            return View(tbl_req_mini);
        }
        public string GetFormattedMessageHTML()
        {
            var link = "สามารถกดอนุมัติได้จาก <a href=https://localhost:44321/ >คลิกที่นี่" + "</a>";

            return "<b> เรียนผู้จัดการฝ่าย  </b>" + "</br>" +
                "แบบฟอร์มการขอและยกเลิกการใช้เครื่อง PDJMINI02 : " + "<br />" +
                "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
               "รหัสพนักงานผู้แจ้ง : " + Session["USER_NO"].ToString() + "<br />" +
                "ชื่อผู้แจ้ง : " + Session["USER_NAME"].ToString() + "<br />" +
                "วันที่แจ้ง : " + ViewBag.Date + "<br />" +
              "รหัสพนักงานผู้ขอสิทธิ : " + ViewBag.Requester + "<br />" +
                 link + "<br />" +
                 "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                 "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";

        }
        // GET: ReqMini/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_req_mini tbl_req_mini = db.tbl_req_mini.Find(id);
            if (tbl_req_mini == null)
            {
                return HttpNotFound();
            }
            return View(tbl_req_mini);
        }

        // POST: ReqMini/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( tbl_req_mini tbl_req_mini)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_req_mini).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.EmpCount = tbl_req_mini.MINI_CODE.ToString();
                var email = Session["USER_EMAIL"].ToString();
                MailMessage mm = new MailMessage();
                mm.To.Add("ITservice@pranda.co.th");
                mm.From = new MailAddress(email);
                mm.Subject = "แบบฟอร์มการขอและยกเลิกการใช้เครื่อง PDJMINI02";

                mm.IsBodyHtml = true;
                mm.Body = GetFormattedMessageIT();

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
            return View(tbl_req_mini);
        }
        public string GetFormattedMessageIT()
        {

            return "<b> เรียน IT SUPPORT </b>" + "</br>" +
              "แบบฟอร์มการขอและยกเลิกการใช้เครื่อง PDJMINI02 : " + "<br />" +
                "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
               "รหัสพนักงานผู้แจ้ง : " + Session["USER_NO"].ToString() + "<br />" +
                "ชื่อผู้แจ้ง : " + Session["USER_NAME"].ToString() + "<br />" +
                 "ได้รับการอนุมัติจากผุ้จัดการฝ่ายเรียบร้อยแล้ว" + "<br/>" +
                 "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                 "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";

        }

        // GET: ReqMini/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_req_mini tbl_req_mini = db.tbl_req_mini.Find(id);
            if (tbl_req_mini == null)
            {
                return HttpNotFound();
            }
            return View(tbl_req_mini);
        }

        // POST: ReqMini/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_req_mini tbl_req_mini = db.tbl_req_mini.Find(id);
            db.tbl_req_mini.Remove(tbl_req_mini);
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
        public ActionResult Details_Print(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_mini_follow View_req_mini_follow = db.View_req_mini_follow.Find(id);
            if (View_req_mini_follow == null)
            {
                return HttpNotFound();
            }
            return View(View_req_mini_follow);
        }
        public ActionResult PrintPartialViewToPdf(string id)
        {
            using (IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities())
            {
                View_req_mini_follow customer = db.View_req_mini_follow.FirstOrDefault(c => c.MINI_CODE == id);

                var report = new PartialViewAsPdf("~/Views/ReqMini/Details_Print.cshtml", customer);
                return report;
            }

        }
    }
}
