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

        public AssetOdometersController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: AssetOdometers
        [HttpPost]
        public ActionResult Index()
        {
            var assetOdometers = db.AssetOdometers;
            return Json(assetOdometers.ToList());
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
