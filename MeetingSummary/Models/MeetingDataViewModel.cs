using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingSummary.Models
{
    public class MeetingDataViewModel
    {
        public MeetingDataViewModel(MeetingData meetingData)
        {
            Id = meetingData.Id;
            CreationDate = meetingData.CreationDate.ToShortDateString();
            Summary = meetingData.MeetingSummary;
        }

        public int Id;
        public string CreationDate;
        public string Summary;
    }
}