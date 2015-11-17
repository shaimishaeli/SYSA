using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingSummary.Models
{
    public class Repository
    {
        public readonly MeetingEntities _db = new MeetingEntities();

        private IEnumerable<MeetingData> GetMeetingsData()
        {
            return _db.MeetingData.OrderBy(x => x.CreationDate);
        }

        public IEnumerable<MeetingDataViewModel> PopulateMeetingReportViewModel()
        {
            var meetingData = GetMeetingsData();
            var meetingReportData = meetingData.Select(x => new MeetingDataViewModel(x));

            return meetingReportData;
        }
    }
}