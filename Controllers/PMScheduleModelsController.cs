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
    public class PMScheduleModelsController : Controller
    {
        private Session3FinalEntities db = new Session3FinalEntities();

        // GET: PMScheduleModels
        public ActionResult Index()
        {
            var pMScheduleModels = db.PMScheduleModels.Include(p => p.PMScheduleType);
            return View(pMScheduleModels.ToList());
        }

        // GET: PMScheduleModels/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMScheduleModel pMScheduleModel = db.PMScheduleModels.Find(id);
            if (pMScheduleModel == null)
            {
                return HttpNotFound();
            }
            return View(pMScheduleModel);
        }

        // GET: PMScheduleModels/Create
        public ActionResult Create()
        {
            ViewBag.PMScheduleTypeID = new SelectList(db.PMScheduleTypes, "ID", "Name");
            return View();
        }

        // POST: PMScheduleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,PMScheduleTypeID")] PMScheduleModel pMScheduleModel)
        {
            if (ModelState.IsValid)
            {
                db.PMScheduleModels.Add(pMScheduleModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PMScheduleTypeID = new SelectList(db.PMScheduleTypes, "ID", "Name", pMScheduleModel.PMScheduleTypeID);
            return View(pMScheduleModel);
        }

        // GET: PMScheduleModels/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMScheduleModel pMScheduleModel = db.PMScheduleModels.Find(id);
            if (pMScheduleModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.PMScheduleTypeID = new SelectList(db.PMScheduleTypes, "ID", "Name", pMScheduleModel.PMScheduleTypeID);
            return View(pMScheduleModel);
        }

        // POST: PMScheduleModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,PMScheduleTypeID")] PMScheduleModel pMScheduleModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pMScheduleModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PMScheduleTypeID = new SelectList(db.PMScheduleTypes, "ID", "Name", pMScheduleModel.PMScheduleTypeID);
            return View(pMScheduleModel);
        }

        // GET: PMScheduleModels/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PMScheduleModel pMScheduleModel = db.PMScheduleModels.Find(id);
            if (pMScheduleModel == null)
            {
                return HttpNotFound();
            }
            return View(pMScheduleModel);
        }

        // POST: PMScheduleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PMScheduleModel pMScheduleModel = db.PMScheduleModels.Find(id);
            db.PMScheduleModels.Remove(pMScheduleModel);
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
