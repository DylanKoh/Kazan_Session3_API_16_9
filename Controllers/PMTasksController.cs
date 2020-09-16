using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kazan_Session3_API_16_9;

namespace Kazan_Session3_API_16_9.Controllers
{
    public class PMTasksController : Controller
    {
        private Session3FinalEntities db = new Session3FinalEntities();

        // GET: PMTasks
        public ActionResult Index()
        {
            var pMTasks = db.PMTasks.Include(p => p.Asset).Include(p => p.PMScheduleType).Include(p => p.Task);
            return View(pMTasks.ToList());
        }

        // GET: PMTasks/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMTask pMTask = db.PMTasks.Find(id);
            if (pMTask == null)
            {
                return HttpNotFound();
            }
            return View(pMTask);
        }

        // GET: PMTasks/Create
        public ActionResult Create()
        {
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN");
            ViewBag.PMScheduleTypeID = new SelectList(db.PMScheduleTypes, "ID", "Name");
            ViewBag.TaskID = new SelectList(db.Tasks, "ID", "Name");
            return View();
        }

        // POST: PMTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AssetID,TaskID,PMScheduleTypeID,ScheduleDate,ScheduleKilometer,TaskDone")] PMTask pMTask)
        {
            if (ModelState.IsValid)
            {
                db.PMTasks.Add(pMTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN", pMTask.AssetID);
            ViewBag.PMScheduleTypeID = new SelectList(db.PMScheduleTypes, "ID", "Name", pMTask.PMScheduleTypeID);
            ViewBag.TaskID = new SelectList(db.Tasks, "ID", "Name", pMTask.TaskID);
            return View(pMTask);
        }

        // GET: PMTasks/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMTask pMTask = db.PMTasks.Find(id);
            if (pMTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN", pMTask.AssetID);
            ViewBag.PMScheduleTypeID = new SelectList(db.PMScheduleTypes, "ID", "Name", pMTask.PMScheduleTypeID);
            ViewBag.TaskID = new SelectList(db.Tasks, "ID", "Name", pMTask.TaskID);
            return View(pMTask);
        }

        // POST: PMTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AssetID,TaskID,PMScheduleTypeID,ScheduleDate,ScheduleKilometer,TaskDone")] PMTask pMTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pMTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN", pMTask.AssetID);
            ViewBag.PMScheduleTypeID = new SelectList(db.PMScheduleTypes, "ID", "Name", pMTask.PMScheduleTypeID);
            ViewBag.TaskID = new SelectList(db.Tasks, "ID", "Name", pMTask.TaskID);
            return View(pMTask);
        }

        // GET: PMTasks/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMTask pMTask = db.PMTasks.Find(id);
            if (pMTask == null)
            {
                return HttpNotFound();
            }
            return View(pMTask);
        }

        // POST: PMTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PMTask pMTask = db.PMTasks.Find(id);
            db.PMTasks.Remove(pMTask);
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
