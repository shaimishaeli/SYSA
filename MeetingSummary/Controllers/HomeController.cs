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
            _repository.SaveSummary(meetingSummary, tasks, assignments, users);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadMeetingData(int? id = null)
        {
            MeetingData meetingData = null;

            if (id.HasValue)
            {
                meetingData = _repository.GetMeetingById(id.Value);
            }
            ViewBag.Users = _repository.GetUsers();

            return PartialView("NewSummary", meetingData);
        }

        [HttpPost]
        public ActionResult UpdateMeeting(int meetingId, string meetingSummary, List<string> tasks, List<string> assignments, List<int> users)
        {
            _repository.UpdateMeetingData(meetingId, meetingSummary, tasks, assignments, users);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}