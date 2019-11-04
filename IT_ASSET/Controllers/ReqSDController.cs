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
using Rotativa;

namespace IT_ASSET.Controllers
{
    public class ReqSDController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        string connectionString = "Data Source= COMP05\\SQLEXPRESS;Initial Catalog=IT_ASSET_SERVER;Integrated Security=True";

        // GET: ReqSD
        public ActionResult Index()
        {
            return View(db.View_req_sd.OrderByDescending(s => s.SD_CODE).ToList());
        }
        public ActionResult IndexSD()
        {
            return View(db.View_req_sd_follow.OrderByDescending(s => s.SD_CODE).ToList());
        }
        // GET: ReqSD/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_sd tbl_req_sd = db.View_req_sd.Find(id);
            if (tbl_req_sd == null)
            {
                return HttpNotFound();
            }
            return View(tbl_req_sd);
        }

        public ActionResult DetailsSD(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_sd_follow View_req_sd_follow = db.View_req_sd_follow.Find(id);
            if (View_req_sd_follow == null)
            {
                return HttpNotFound();
            }
            return View(View_req_sd_follow);
        }
        public ActionResult DetailsSD_Print(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_sd_follow View_req_sd_follow = db.View_req_sd_follow.Find(id);
            if (View_req_sd_follow == null)
            {
                return HttpNotFound();
            }
            return View(View_req_sd_follow);
        }
        public ActionResult PrintPartialViewToPdf(string id)
        {
            using (IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities())
            {
                View_req_sd_follow customer = db.View_req_sd_follow.FirstOrDefault(c => c.SD_CODE == id);

                var report = new PartialViewAsPdf("~/Views/ReqSD/DetailsSD_Print.cshtml", customer);
                return report;
            }

        }

        // GET: ReqSD/Create
        public ActionResult Create()
        {
            ViewData["ALLOW_STATUS"] = new SelectList(db.tbl_req_allow_status, "ALLOW_STATUS", "ALLOW_DESC");
           

            return View();
        }

        // POST: ReqSD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_req_sd tbl_req_sd)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddReqShareDrive", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USER_NO", tbl_req_sd.USER_NO);
                    cmd.Parameters.AddWithValue("@Allow_STATUS", tbl_req_sd.ALLOW_STATUS);
                    cmd.Parameters.AddWithValue("@SD_DRIVE", tbl_req_sd.SD_DRIVE);
                    cmd.Parameters.AddWithValue("@SD_FOLDER", tbl_req_sd.SD_FOLDER);
                    cmd.Parameters.AddWithValue("@REQ_AUTH", tbl_req_sd.REQ_AUTH);
                    cmd.Parameters.AddWithValue("@REQ_STATUS", tbl_req_sd.REQ_STATUS);
                    cmd.Parameters.AddWithValue("@SD_NOTE", tbl_req_sd.SD_NOTE);
                    cmd.Parameters.AddWithValue("@SD_REQUESTER", tbl_req_sd.SD_REQUESTER);
                    cmd.Parameters.AddWithValue("@SD_DATE", tbl_req_sd.SD_DATE);

                    cmd.Parameters.Add("@SD_CODE", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@SD_CODE"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    ViewBag.EmpCount = cmd.Parameters["@SD_CODE"].Value.ToString();

                    var email = Session["USER_EMAIL"].ToString();
                    var approve = Session["USER_EMAIL_APPROVE"].ToString();
                    MailMessage mm = new MailMessage();
                    mm.To.Add(approve);
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
                }
                   
                return RedirectToAction("Index");
            }
            ViewData["ALLOW_STATUS"] = new SelectList(db.tbl_req_allow_status, "ALLOW_STATUS", "ALLOW_DESC", tbl_req_sd.ALLOW_STATUS);
           


            return View(tbl_req_sd);
        }
        private string GetFormattedMessageHTML()
        {
            var link = "สามารถกดอนุมัติได้จาก <a href=https://localhost:44321/ >คลิกที่นี่" + "</a>";
            return "<b> เรียนผู้จัดการฝ่าย  </b>" + "</br>" +
                "การขอสิทธิ ALFRESCO : " + Session["USER_NAME"].ToString() + "<br />" +
                 "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +
                 "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                 link + "<br />" +
                 "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                 "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";

        }

        // GET: ReqSD/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_req_sd tbl_req_sd = db.tbl_req_sd.Find(id);
            if (tbl_req_sd == null)
            {
                return HttpNotFound();
            }
            return View(tbl_req_sd);
        }

        // POST: ReqSD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_req_sd tbl_req_sd)
        {
 if (ModelState.IsValid)
            {
                db.Entry(tbl_req_sd).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.EmpCount = tbl_req_sd.SD_CODE.ToString();
                var email = Session["USER_EMAIL"].ToString();
                MailMessage mm = new MailMessage();
                mm.To.Add("ITservice@pranda.co.th");
                mm.From = new MailAddress(email);
                mm.Subject = "แบบฟอร์มการขอและยกเลิกรหัสผู้ใช้ เพื่อกำหนดเข้าสู่ Share Drive";

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
        
           
            return View(tbl_req_sd);
        }
        private string GetFormattedMessageIT()
        {
            return "<b> เรียน IT SUPPORT </b>" + "</br>" +
                "การขอสิทธิ ALFRESCO : " + Session["USER_NAME"].ToString() + "<br />" +
                 "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +
                 "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                 "ได้รับการอนุมัติจากผุ้จัดการฝ่ายเรียบร้อยแล้ว" + "<br/>" +
                 "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                 "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";

        }

        // GET: ReqSD/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_req_sd tbl_req_sd = db.tbl_req_sd.Find(id);
            if (tbl_req_sd == null)
            {
                return HttpNotFound();
            }
            return View(tbl_req_sd);
        }

        // POST: ReqSD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_req_sd tbl_req_sd = db.tbl_req_sd.Find(id);
            db.tbl_req_sd.Remove(tbl_req_sd);
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
        public ActionResult Report()

        {
            Response.ClearContent();
            Response.ContentType = "application/fore-download";
            Response.AddHeader("content-disposition", " attachment; Filename=" + "REPORT_ShareDrive" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
            Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            Response.Write("<head>");
            Response.Write("<META http-equiv=\"content-Type\" content=\"text/html; charset=utf-8\">");
            Response.Write("<!--[if gte mso 9]><xml>");
            Response.Write("<x:ExcelWorkbook>");
            Response.Write("<x:ExcelWorksheets>");
            Response.Write("<x:ExcelWorksheet>");
            Response.Write("<x:Name>Report Data</x:Name>");
            Response.Write("<x:WorksheetOptions>");
            Response.Write("<x:Print>");
            Response.Write("<x:ValidprinterInfo/>");
            Response.Write("<x:Print>");
            Response.Write("<x:WorksheetOptions>");
            Response.Write("<x:ExcelWorksheet>");
            Response.Write("<x:ExcelWorksheets>");
            Response.Write("<x:ExcelWorkbook>");
            Response.Write("</xml>");
            Response.Write("<![endif] --> ");


            View("~/Views/ReqSD/Report.cshtml", db.View_req_sd_follow.ToList()).ExecuteResult(this.ControllerContext);
            Response.Flush();
            Response.End();
            return View();


        }
    }
}
