using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_ASSET.Models;
using PagedList.Mvc;
using PagedList;
using System.Data.SqlClient;

namespace IT_ASSET.Controllers
{
    public class AllincController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        //string connectionString = "Data Source= COMP05\\SQLEXPRESS;Initial Catalog=IT_ASSET_MANAGEMENT;Integrated Security=True";
        // GET: Allinc
        public ActionResult Index(int? i,string search)
        {
           
            return View(db.View_incident_all.OrderByDescending(s => s.INC_DATE).Where(s => s.INC_CODE.Contains(search) || s.INC_TOPIC.Contains(search) || s.INC_REQUESTER.Contains(search)|| search == null).ToList().ToPagedList(i ?? 1, 12)) ;
        }

        // GET: Allinc/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_incident_all view_incident_all = db.View_incident_all.SingleOrDefault(m => m.INC_CODE == id);
            if (view_incident_all == null)
            {
                return HttpNotFound();
            }
            return View(view_incident_all);
        }

        // GET: Allinc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Allinc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "INC_CODE,INC_DATE,INC_TOPIC,INC_REQUESTER,INC_OPEN_BY,INC_ASSIGN_TO,INC_STATUS,INC_CATEGORIES,INC_BILL_NO,INC_BILL_DATE,INC_OPEN_DATE,INC_DUE_DATE,INC_RESOLVE_DATE,INC_DESCRIPTION,HW_CODE,INC_CLOSE_DATE,COST_ID,INC_SUBMIT_DATE,INC_ASSIGN_DATE,COST_SECTION,INC_CANCEL_DATE,STA_DESCRIPTION,EMP_NO,IT_NAME,IT_SURNAME,EMP_ASSIGN,EMP_ASSIGN_NAME,EMP_ASSIGN_SURNAME")] View_incident_all view_incident_all)
        {
            if (ModelState.IsValid)
            {
                db.View_incident_all.Add(view_incident_all);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_incident_all);
        }

        // GET: Allinc/Edit/5
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
            ViewData["INC_STATUS"] = new SelectList(db.tbl_Incident_status, "INC_STATUS", "STA_DESCRIPTION","07");
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
           
            return View(tbl_incident_case);
        }

        // GET: Allinc/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_incident_all view_incident_all = db.View_incident_all.Find(id);
            if (view_incident_all == null)
            {
                return HttpNotFound();
            }
            return View(view_incident_all);
        }

        // POST: Allinc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            View_incident_all view_incident_all = db.View_incident_all.Find(id);
            db.View_incident_all.Remove(view_incident_all);
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
