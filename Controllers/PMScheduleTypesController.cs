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
    public class PMScheduleTypesController : Controller
    {
        private Session3FinalEntities db = new Session3FinalEntities();

        // GET: PMScheduleTypes
        public ActionResult Index()
        {
            return View(db.PMScheduleTypes.ToList());
        }

        // GET: PMScheduleTypes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMScheduleType pMScheduleType = db.PMScheduleTypes.Find(id);
            if (pMScheduleType == null)
            {
                return HttpNotFound();
            }
            return View(pMScheduleType);
        }

        // GET: PMScheduleTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PMScheduleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] PMScheduleType pMScheduleType)
        {
            if (ModelState.IsValid)
            {
                db.PMScheduleTypes.Add(pMScheduleType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pMScheduleType);
        }

        // GET: PMScheduleTypes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMScheduleType pMScheduleType = db.PMScheduleTypes.Find(id);
            if (pMScheduleType == null)
            {
                return HttpNotFound();
            }
            return View(pMScheduleType);
        }

        // POST: PMScheduleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] PMScheduleType pMScheduleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pMScheduleType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pMScheduleType);
        }

        // GET: PMScheduleTypes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMScheduleType pMScheduleType = db.PMScheduleTypes.Find(id);
            if (pMScheduleType == null)
            {
                return HttpNotFound();
            }
            return View(pMScheduleType);
        }

        // POST: PMScheduleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PMScheduleType pMScheduleType = db.PMScheduleTypes.Find(id);
            db.PMScheduleTypes.Remove(pMScheduleType);
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
