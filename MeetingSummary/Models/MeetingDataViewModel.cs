using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MeetingSummary.Models
{
    public class MeetingDataViewModel
    {
        public MeetingDataViewModel(MeetingData meetingData)
        {
            string format = new CultureInfo("he-IL").DateTimeFormat.ShortDatePattern;
            Id = meetingData.Id;
            CreationDate = meetingData.CreationDate.ToString(format);
            UpdateDate = meetingData.UpdateDate.HasValue ? meetingData.UpdateDate.Value.ToString(format) : string.Empty;
            Subject = meetingData.MeetingSubject;
            Summary = meetingData.MeetingSummary;
        }

        public int Id;
        public string CreationDate;
        public string Subject;
        public string UpdateDate;
        public string Summary;
    }
}