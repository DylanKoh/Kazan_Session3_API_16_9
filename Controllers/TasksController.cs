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
    public class TasksController : Controller
    {
        private Session3FinalEntities db = new Session3FinalEntities();

        public TasksController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Tasks
        [HttpPost]
        public ActionResult Index()
        {
            return Json(db.Tasks.ToList());
        }

        // POST: Tasks/Details/5
        [HttpPost]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return Json(task);
        }

        // POST: Tasks/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,Name")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return Json("Task created successfully!");
            }

            return Json("Unable to create task!");
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Name")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Edited Task successfully!");
            }
            return Json("An error occured while editing task! Please try again!");
        }


        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return Json("Deleted Task successfully!");
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
