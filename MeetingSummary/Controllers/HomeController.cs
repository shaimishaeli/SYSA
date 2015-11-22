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
        public ActionResult SaveNewSummary(string creationDate, string meetingSubject, string meetingSummary, List<string> tasks, List<string> assignments, List<int> users)
        {
            _repository.SaveSummary(creationDate, meetingSubject, meetingSummary, tasks, assignments, users);
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
            ViewBag.UsersData = _repository.GetUsers();

            return PartialView("NewSummary", meetingData);
        }

        [HttpPost]
        public ActionResult UpdateMeeting(string creationDate, string meetingSubject, int meetingId, string meetingSummary, List<string> tasks, List<string> assignments, List<int> users, List<bool> tasksChk, List<bool> assignmentsChk)
        {
            _repository.UpdateMeetingData(creationDate, meetingSubject, meetingId, meetingSummary, tasks, assignments, users, tasksChk, assignmentsChk);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMeetingData(int meetingId)
        {
            _repository.DeleteMeetingData(meetingId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadModalMessage(string modalTitle, string modalMessage, string action)
        {
            ViewBag.ModalTitle = modalTitle;
            ViewBag.ModalMessage = modalMessage;
            ViewBag.Action = action;
            return PartialView("ModalMessage");
        }

        [HttpPost]
        public ActionResult AddComponent(string type)
        {
            ViewBag.UsersData = _repository.GetUsers();
            return PartialView(type);
        }
    }
}