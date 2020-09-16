using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Kazan_Session3_API_16_9;

namespace Kazan_Session3_API_16_9.Controllers
{
    public class PMTasksController : Controller
    {
        private Session3FinalEntities db = new Session3FinalEntities();

        public PMTasksController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: PMTasks
        [HttpPost]
        public ActionResult Index()
        {
            var pMTasks = db.PMTasks;
            return Json(pMTasks.ToList());
        }

        // POST: PMTasks/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,AssetID,TaskID,PMScheduleTypeID,ScheduleDate,ScheduleKilometer,TaskDone")] PMTask pMTask)
        {
            if (ModelState.IsValid)
            {
                if (pMTask.PMScheduleTypeID == 1)
                {
                    var findCurrentTask = (from x in db.PMTasks
                                           where x.AssetID == pMTask.AssetID && x.TaskID == pMTask.TaskID
                                           where x.ScheduleKilometer > pMTask.ScheduleKilometer
                                           select x).FirstOrDefault();
                    if (findCurrentTask != null)
                    {
                        return Json("Unable to add same task within similar range of values!");
                    }
                    else
                    {
                        db.PMTasks.Add(pMTask);
                        db.SaveChanges();
                        return Json("Created PM Task successfully!");
                    }
                }
                else
                {
                    db.PMTasks.Add(pMTask);
                    db.SaveChanges();
                    return Json("Created PM Task successfully!");
                }

            }
            return Json("There was an error during creation of PM Task! PLease contact our administrator!");
        }


        // POST: PMTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            PMTask pMTask = db.PMTasks.Find(id);
            db.PMTasks.Remove(pMTask);
            db.SaveChanges();
            return Json("Successfully deleted task!");
        }

        // POST: PMTasks/GetRunTaskNotDone
        [HttpPost]
        public ActionResult GetRunTaskNotDone()
        {
            var getCustom = (from x in db.PMTasks
                             where x.TaskDone == false && x.ScheduleKilometer != null
                             join y in db.PMScheduleTypes on x.PMScheduleTypeID equals y.ID
                             join z in db.Assets on x.AssetID equals z.ID
                             join a in db.Tasks on x.TaskID equals a.ID
                             select new
                             {
                                 PMTaskID = x.ID,
                                 Asset = z.AssetName + "** SN:" + z.AssetSN,
                                 TaskName = a.Name,
                                 TaskTypeAndValue = y.Name + " - at " + x.ScheduleKilometer + " Kilometer",
                                 TaskDone = x.TaskDone,
                                 bgColour = "Gray"
                             }).ToList();
            return Json(getCustom);
        }

        // POST: PMTasks/GetOverTime?activeDate={}
        [HttpPost]
        public ActionResult GetOverTime(DateTime activeDate)
        {
            var getCustom = (from x in db.PMTasks
                             where x.TaskDone == false && x.ScheduleDate != null && x.ScheduleDate < activeDate
                             join y in db.PMScheduleTypes on x.PMScheduleTypeID equals y.ID
                             join z in db.Assets on x.AssetID equals z.ID
                             join a in db.Tasks on x.TaskID equals a.ID
                             select new
                             {
                                 PMTaskID = x.ID,
                                 Asset = z.AssetName + "** SN:" + z.AssetSN,
                                 TaskName = a.Name,
                                 TaskTypeAndValue = y.Name + " - at " + x.ScheduleDate,
                                 TaskDone = x.TaskDone,
                                 bgColour = "Red"
                             }).ToList();
            return Json(getCustom);
        }

        // POST: PMTasks/GetCurrentTime?activeDate={}
        [HttpPost]
        public ActionResult GetCurrentTime(DateTime activeDate)
        {
            var getCustom = (from x in db.PMTasks
                             where x.TaskDone == false && x.ScheduleDate != null && x.ScheduleDate == activeDate
                             join y in db.PMScheduleTypes on x.PMScheduleTypeID equals y.ID
                             join z in db.Assets on x.AssetID equals z.ID
                             join a in db.Tasks on x.TaskID equals a.ID
                             select new
                             {
                                 PMTaskID = x.ID,
                                 Asset = z.AssetName + "** SN:" + z.AssetSN,
                                 TaskName = a.Name,
                                 TaskTypeAndValue = y.Name + " - at " + x.ScheduleDate,
                                 TaskDone = x.TaskDone,
                                 bgColour = "Gray"
                             }).ToList();
            return Json(getCustom);
        }

        // POST: PMTasks/GetAboutTime?activeDate={}
        [HttpPost]
        public ActionResult GetAboutTime(DateTime activeDate)
        {
            var getList = (from x in db.PMTasks
                           select x).ToList();

            var getCustom = (from x in getList
                             where x.TaskDone == false && x.ScheduleDate != null && (x.ScheduleDate - activeDate).Value.TotalDays <= 4
                             join y in db.PMScheduleTypes on x.PMScheduleTypeID equals y.ID
                             join z in db.Assets on x.AssetID equals z.ID
                             join a in db.Tasks on x.TaskID equals a.ID
                             select new
                             {
                                 PMTaskID = x.ID,
                                 Asset = z.AssetName + "** SN:" + z.AssetSN,
                                 TaskName = a.Name,
                                 TaskTypeAndValue = y.Name + " - at " + x.ScheduleDate,
                                 TaskDone = x.TaskDone,
                                 bgColour = "Gray"
                             }).ToList();
            return Json(getCustom);
        }

        // POST: PMTasks/GetRunTaskDone
        [HttpPost]
        public ActionResult GetRunTaskDone()
        {
            var getCustom = (from x in db.PMTasks
                             where x.TaskDone == true && x.ScheduleKilometer != null
                             join y in db.PMScheduleTypes on x.PMScheduleTypeID equals y.ID
                             join z in db.Assets on x.AssetID equals z.ID
                             join a in db.Tasks on x.TaskID equals a.ID
                             select new
                             {
                                 PMTaskID = x.ID,
                                 Asset = z.AssetName + "** SN:" + z.AssetSN,
                                 TaskName = a.Name,
                                 TaskTypeAndValue = y.Name + " - at " + x.ScheduleKilometer + " Kilometer",
                                 TaskDone = x.TaskDone,
                                 bgColour = "DarkGray"
                             }).ToList();
            return Json(getCustom);
        }

        // POST: PMTask/GetTimeDone
        [HttpPost]
        public ActionResult GetTimeDone()
        {
            var getCustom = (from x in db.PMTasks
                             where x.TaskDone == true && x.ScheduleDate != null
                             join y in db.PMScheduleTypes on x.PMScheduleTypeID equals y.ID
                             join z in db.Assets on x.AssetID equals z.ID
                             join a in db.Tasks on x.TaskID equals a.ID
                             select new
                             {
                                 PMTaskID = x.ID,
                                 Asset = z.AssetName + "** SN:" + z.AssetSN,
                                 TaskName = a.Name,
                                 TaskTypeAndValue = y.Name + " - at " + x.ScheduleDate,
                                 TaskDone = x.TaskDone,
                                 bgColour = "Green"
                             }).ToList();
            return Json(getCustom);
        }

        // POST: PMTasks/UpdateDone?PMID={}&changedState={}
        [HttpPost]
        public ActionResult UpdateDone(long PMID, bool changedState)
        {
            var getPMTask = (from x in db.PMTasks
                             where x.ID == PMID
                             select x).FirstOrDefault();
            if (changedState != getPMTask.TaskDone)
            {
                getPMTask.TaskDone = true;
                db.SaveChanges();
                return Json("Completed Update!");
            }
            else
            {
                return Json("Update not needed!");
            }
        }

        // POST: PMTasks/UpdateNotDone?PMID={}&changedState={}
        [HttpPost]
        public ActionResult UpdateNotDone(long PMID, bool changedState)
        {
            var getPMTask = (from x in db.PMTasks
                             where x.ID == PMID
                             select x).FirstOrDefault();
            if (changedState != getPMTask.TaskDone)
            {
                getPMTask.TaskDone = false;
                db.SaveChanges();
                return Json("Completed Update!");
            }
            else
            {
                return Json("Update not needed!");
            }

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
