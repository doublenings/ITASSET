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
    public class tbl_incident_caseController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();

        // GET: tbl_incident_case
        public ActionResult Index()
        {
            return View(db.tbl_incident_case.ToList());
        }

        // GET: tbl_incident_case/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_incident_case tbl_incident_case = db.tbl_incident_case.Find(id);
            if (tbl_incident_case == null)
            {
                return HttpNotFound();
            }
            return View(tbl_incident_case);
        }

        // GET: tbl_incident_case/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_incident_case/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "INC_ID,INC_CODE,INC_DATE,INC_TOPIC,INC_REQUESTER,INC_OPEN_BY,INC_ASSIGN_TO,INC_STATUS,INC_CATEGORIES,INC_PRIORITY,INC_BILL_NO,INC_BILL_DATE,INC_OPEN_DATE,INC_DUE_DATE,INC_RESOLVE_DATE,INC_COST,INC_WARANTEE,INC_DESCRIPTION,INC_COMMENTS,USER_CREATE,USER_UPDATE,CREATE_DATE,UPDATE_DATE,upsize_ts,HW_CODE,INC_CLOSE_DATE,COST_ID,INC_SUBMIT_DATE,INC_ASSIGN_DATE,COST_SECTION,INC_CANCEL_DATE")] tbl_incident_case tbl_incident_case)
        {
            if (ModelState.IsValid)
            {
                db.tbl_incident_case.Add(tbl_incident_case);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_incident_case);
        }

        // GET: tbl_incident_case/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_incident_case tbl_incident_case = db.tbl_incident_case.Find(id);
            if (tbl_incident_case == null)
            {
                return HttpNotFound();
            }
            return View(tbl_incident_case);
        }

        // POST: tbl_incident_case/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "INC_ID,INC_CODE,INC_DATE,INC_TOPIC,INC_REQUESTER,INC_OPEN_BY,INC_ASSIGN_TO,INC_STATUS,INC_CATEGORIES,INC_PRIORITY,INC_BILL_NO,INC_BILL_DATE,INC_OPEN_DATE,INC_DUE_DATE,INC_RESOLVE_DATE,INC_COST,INC_WARANTEE,INC_DESCRIPTION,INC_COMMENTS,USER_CREATE,USER_UPDATE,CREATE_DATE,UPDATE_DATE,upsize_ts,HW_CODE,INC_CLOSE_DATE,COST_ID,INC_SUBMIT_DATE,INC_ASSIGN_DATE,COST_SECTION,INC_CANCEL_DATE")] tbl_incident_case tbl_incident_case)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_incident_case).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["INC_STATUS"] = new SelectList(db.tbl_Incident_status, "INC_STATUS", "STA_DESCRIPTION");
            return View(tbl_incident_case);
        }

        // GET: tbl_incident_case/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_incident_case tbl_incident_case = db.tbl_incident_case.Find(id);
            if (tbl_incident_case == null)
            {
                return HttpNotFound();
            }
            return View(tbl_incident_case);
        }

        // POST: tbl_incident_case/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_incident_case tbl_incident_case = db.tbl_incident_case.Find(id);
            db.tbl_incident_case.Remove(tbl_incident_case);
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
