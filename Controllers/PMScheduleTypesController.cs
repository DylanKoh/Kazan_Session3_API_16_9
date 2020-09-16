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

        public PMScheduleTypesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: PMScheduleTypes
        [HttpPost]
        public ActionResult Index()
        {
            return Json(db.PMScheduleTypes.ToList());
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
