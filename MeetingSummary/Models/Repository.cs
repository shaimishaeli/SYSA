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

            UpdateData(meetingData.Id, tasks, assignments, users);
        }

        public void UpdateData(int meetingId, List<string> tasks, List<string> assignments, List<int> users)
        {
            if(tasks != null)
            {
                foreach (var task in tasks)
                {
                    var taskObj = new MeetingTasks();
                    taskObj.Description = task;
                    taskObj.MeetingId = meetingId;
                    _db.MeetingTasks.Add(taskObj);
                }
            }

            if (assignments != null)
            {
                for (int i = 0; i < assignments.Count; i++)
                {
                    var assignmentObj = new MeetingAssignments();
                    assignmentObj.Description = assignments[i];
                    assignmentObj.AssignedToUserId = users[i];
                    assignmentObj.MeetingId = meetingId;
                    _db.MeetingAssignments.Add(assignmentObj);
                }
            }

            _db.SaveChanges();
        }

        public MeetingData GetMeetingById(int id)
        {
            return _db.MeetingData.Find(id);
        }

        public void UpdateMeetingData(int meetingId, string meetingSummary, List<string> tasks, List<string> assignments, List<int> users)
        {
            var meetingData = GetMeetingById(meetingId);
            meetingData.UpdateDate = DateTime.Now;
            meetingData.MeetingSummary = meetingSummary;

            var tasksList = meetingData.MeetingTasks.ToList();
            foreach (var task in tasksList)
            {
                _db.MeetingTasks.Remove(task);
            }

            var assignmentsList = meetingData.MeetingAssignments.ToList();
            foreach (var assignment in assignmentsList)
            {
                _db.MeetingAssignments.Remove(assignment);
            }

            _db.SaveChanges();

            UpdateData(meetingId, tasks, assignments, users);
        }
    }
}