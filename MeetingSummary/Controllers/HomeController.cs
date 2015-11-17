using MeetingSummary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetingSummary.Controllers
{
    public class HomeController : Controller
    {
        public readonly Repository _repository = new Repository();
        public ActionResult Index()
        {
            ViewBag.Users = new SelectList(_repository.GetUsers(), "Id", "Email");
            return View();
        }

        [HttpPost]
        public ActionResult LoadMeetingsReport()
        {
            var meetingDataModel = _repository.PopulateMeetingReportViewModel();

            return Json(new { data = meetingDataModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveNewSummary(string meetingSummary, List<string> tasks, List<string> assignments, List<int> users)
        {
            //_repository.SaveSummary(meetingSummary, tasks, assignments, users);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}