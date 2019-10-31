using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_ASSET.Models;

namespace IT_ASSET.Controllers
{
    public class ReqSDController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();

        // GET: ReqSD
        public ActionResult Index()
        {
            return View(db.View_req_sd.ToList());
        }

        // GET: ReqSD/Details/5
        public ActionResult Details(string id)
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
                db.tbl_req_sd.Add(tbl_req_sd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ALLOW_STATUS"] = new SelectList(db.tbl_req_allow_status, "ALLOW_STATUS", "ALLOW_DESC", tbl_req_sd.ALLOW_STATUS);

            
            return View(tbl_req_sd);
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
        public ActionResult Edit([Bind(Include = "SD_ID,SD_CODE,USER_NO,SD_DATE,ALLOW_STATUS,SD_DRIVE,SD_FOLDER,SD_NOTE,REQ_AUTH,SD_REQUESTER,SD_APPROVE_OWNER,SD_APPROVE_OWNER_DATE,SD_APPROVE,SD_APPROVE_DATE,SD_OPEN_BY,SD_OPEN_DATE,SD_ASSIGN_TO,SD_ASSIGN_DATE,SD_SUBMIT_DATE,SD_CLOSE,AF_CLOSE_DATE,REQ_STATUS,USER_CREATE,USER_UPDATE,CREATE_DATE,UPDATE_DATE,AF_IT_COMMENT,AF_IT_COMMENTER,AF_IT_MANAGER,AF_IT_DATE")] tbl_req_sd tbl_req_sd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_req_sd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_req_sd);
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
    }
}
