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
    }
}