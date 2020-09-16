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
    public class AssetOdometersController : Controller
    {
        private Session3FinalEntities db = new Session3FinalEntities();

        // GET: AssetOdometers
        public ActionResult Index()
        {
            var assetOdometers = db.AssetOdometers.Include(a => a.Asset);
            return View(assetOdometers.ToList());
        }

        // GET: AssetOdometers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetOdometer assetOdometer = db.AssetOdometers.Find(id);
            if (assetOdometer == null)
            {
                return HttpNotFound();
            }
            return View(assetOdometer);
        }

        // GET: AssetOdometers/Create
        public ActionResult Create()
        {
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN");
            return View();
        }

        // POST: AssetOdometers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AssetID,ReadDate,OdometerAmount")] AssetOdometer assetOdometer)
        {
            if (ModelState.IsValid)
            {
                db.AssetOdometers.Add(assetOdometer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN", assetOdometer.AssetID);
            return View(assetOdometer);
        }

        // GET: AssetOdometers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetOdometer assetOdometer = db.AssetOdometers.Find(id);
            if (assetOdometer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN", assetOdometer.AssetID);
            return View(assetOdometer);
        }

        // POST: AssetOdometers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AssetID,ReadDate,OdometerAmount")] AssetOdometer assetOdometer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetOdometer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN", assetOdometer.AssetID);
            return View(assetOdometer);
        }

        // GET: AssetOdometers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetOdometer assetOdometer = db.AssetOdometers.Find(id);
            if (assetOdometer == null)
            {
                return HttpNotFound();
            }
            return View(assetOdometer);
        }

        // POST: AssetOdometers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AssetOdometer assetOdometer = db.AssetOdometers.Find(id);
            db.AssetOdometers.Remove(assetOdometer);
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
