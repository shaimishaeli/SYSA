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

        public IEnumerable<Users> GetUsers()
        {
            return _db.Users;
        }

        public void SaveSummary(string meetingSummary, List<string> tasks, List<string> assignments, List<int> users)
        {
            var meetingData = new MeetingData();
            meetingData.MeetingSummary = meetingSummary;
            meetingData.CreationDate = DateTime.Now;
            _db.MeetingData.Add(meetingData);
            _db.SaveChanges();
        }
    }
}