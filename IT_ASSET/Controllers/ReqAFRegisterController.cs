using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using IT_ASSET.Models;
using OfficeOpenXml;
using PagedList;
using Rotativa;

namespace IT_ASSET.Controllers
{
    public class ReqAFRegisterController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        string connectionString = "Data Source= serverjob;Initial Catalog=IT_ASSET_SERVER;Integrated Security=True";
        // GET: ReqAFRegister
        public ActionResult Index(int? i, string search)
        {
            return View(db.View_req_af_register.OrderByDescending(s => s.AF_DATE).Where(s => s.AF_CODE.Contains(search) || s.USER_NO.Contains(search) || s.USER_NAME.Contains(search) || s.AF_REQUESTER.Contains(search)|| search == null).ToList().ToPagedList(i ?? 1, 15));
        }
        public ActionResult IndexAF(int? i, string search)
        {
            return View(db.View_req_af_follow.OrderByDescending(s => s.AF_APPROVE_DATE).Where(s => s.AF_CODE.Contains(search) || s.USER_NO.Contains(search) || s.USER_NAME.Contains(search) || s.AF_REQUESTER.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 15));
        }


        // GET: ReqAFRegister/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_af_register tbl_req_af = db.View_req_af_register.SingleOrDefault(m => m.AF_CODE == id);
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

            ViewData["AF_STATUS"] = new SelectList(db.tbl_req_af_status, "AF_STATUS", "AF_STATUS_NAME");
            return View();
        }

        // POST: ReqAFRegister/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AF tbl_req_af)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddReqAF", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Allow_STATUS", tbl_req_af.ALLOW_STATUS);
                    cmd.Parameters.AddWithValue("@USER_NO", tbl_req_af.USER_NO);
                    cmd.Parameters.AddWithValue("@AF_Site", tbl_req_af.AF_SITE);
                    cmd.Parameters.AddWithValue("@AF_Folder", tbl_req_af.AF_FOLDER);
                    cmd.Parameters.AddWithValue("@AF_Status", tbl_req_af.AF_STATUS);
                    cmd.Parameters.AddWithValue("@REQ_STATUS", tbl_req_af.REQ_STATUS);
                    cmd.Parameters.AddWithValue("@AF_Note", tbl_req_af.AF_NOTE);
                    cmd.Parameters.AddWithValue("@AF_REQUESTER", tbl_req_af.AF_REQUESTER);

                    cmd.Parameters.AddWithValue("@AF_DATE", tbl_req_af.AF_DATE);


                    cmd.Parameters.Add("@AF_CODE", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@AF_CODE"].Direction = ParameterDirection.Output;

                    TempData["msg"] = "<script>alert('Saved succesfully');</script>";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                   
                    ViewBag.EmpCount = cmd.Parameters["@AF_CODE"].Value.ToString();


                    var email = Session["USER_EMAIL"].ToString();
                    var approve = Session["USER_EMAIL_APPROVE"].ToString();
                    MailMessage mm = new MailMessage();
                    mm.To.Add(approve);
                    mm.From = new MailAddress(email);
                    mm.Subject = "แบบฟอร์มการขอและยกเลิกสิทธิใช้ระบบ ALFRESCO";

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
            ViewData["ALLOW_STATUS"] = new SelectList(db.tbl_req_allow_status, "ALLOW_STATUS", "ALLOW_DESC", tbl_req_af.ALLOW_STATUS);

            ViewData["AF_STATUS"] = new SelectList(db.tbl_req_af_status, "AF_STATUS", "AF_STATUS_NAME", tbl_req_af.AF_STATUS);
            return View(tbl_req_af);
        }
        private string GetFormattedMessageHTML()
        {
            var link = "สามารถกดอนุมัติได้จาก <a href=https://localhost:44321/ >คลิกที่นี่"+"</a>";
            return "<b> เรียนผู้จัดการฝ่าย  </b>" + "</br>" +
                "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
               "รหัสพนักงานผู้แจ้ง : " + Session["USER_NO"].ToString() + "<br />" +
                "ชื่อผู้แจ้ง : " + Session["USER_NAME"].ToString() + "<br />" +
                "วันที่แจ้ง : " + ViewBag.Date + "<br />" +
              "รหัสพนักงานผู้ขอสิทธิ : " + ViewBag.Requester + "<br />" +
                 link + "<br />" +
                 "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                 "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";

        }

        private string GetFormattedMessageIT()
        {
            return "<b> เรียน IT SUPPORT </b>" + "</br>" +
                "การขอสิทธิ ALFRESCO : " + Session["USER_NAME"].ToString() + "<br />" +
                 "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
               "รหัสพนักงานผู้แจ้ง : " + Session["USER_NO"].ToString() + "<br />" +
                "ชื่อผู้แจ้ง : " + Session["USER_NAME"].ToString() + "<br />" +
                 "ได้รับการอนุมัติจากผุ้จัดการฝ่ายเรียบร้อยแล้ว" + "<br/>" +
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

            return View(tbl_req_af);
        }

        // POST: ReqAFRegister/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_req_af tbl_req_af)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_req_af).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.EmpCount = tbl_req_af.AF_CODE.ToString();
                var email = Session["USER_EMAIL"].ToString();
                MailMessage mm = new MailMessage();
                mm.To.Add("ITservice@pranda.co.th");
                mm.From = new MailAddress(email);
                mm.Subject = "แบบฟอร์มการขอสิทธิ ALFRESCO";

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
                return RedirectToAction("IndexAF");
            }

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

        public ActionResult Details_AF(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_af_follow view_Req_Af_Follow = db.View_req_af_follow.Find(id);
            if (view_Req_Af_Follow == null)
            {
                return HttpNotFound();
            }
            return View(view_Req_Af_Follow);
        }
        public ActionResult Report()

        {
            Response.ClearContent();
            Response.ContentType = "application/fore-download";
            Response.AddHeader("content-disposition", " attachment; Filename=" + "REPORT_AF"+ DateTime.Now.ToString("ddMMyyyy") + ".xls");
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


            View("~/Views/ReqAFRegister/Report.cshtml", db.View_req_af_follow.ToList()).ExecuteResult(this.ControllerContext);
            Response.Flush();
            Response.End();
            return View();


        }
        public ActionResult Details_Print(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_req_af_follow view_Req_Af_Follow = db.View_req_af_follow.Find(id);
            if (view_Req_Af_Follow == null)
            {
                return HttpNotFound();
            }
            return View(view_Req_Af_Follow);
        }
        public ActionResult PrintPartialViewToPdf(string id)
        {
            using (IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities())
            {
                View_req_af_follow customer = db.View_req_af_follow.FirstOrDefault(c => c.AF_CODE == id);

                var report = new PartialViewAsPdf("~/Views/ReqAFRegister/Details_Print.cshtml", customer);
                return report;
            }

        }
    }

    }

