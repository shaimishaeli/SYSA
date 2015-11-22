using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MeetingSummary.Models
{
    public class Repository
    {
        public readonly MeetingEntities _db = new MeetingEntities();

        private IEnumerable<MeetingData> GetMeetingsData()
        {
            return _db.MeetingData.Where(x => !x.IsArchived).OrderBy(x => x.CreationDate);
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

        public void SaveSummary(string creationDate, string meetingSubject, string meetingSummary, List<string> tasks, List<string> assignments, List<int> users)
        {
            var meetingData = new MeetingData();
            meetingData.MeetingSubject = meetingSubject;
            meetingData.MeetingSummary = meetingSummary;
            meetingData.CreationDate = DateTime.ParseExact(creationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            _db.MeetingData.Add(meetingData);
            _db.SaveChanges();

            UpdateData(meetingData.Id, tasks, assignments, users, null, null);
        }

        public void UpdateData(int meetingId, List<string> tasks, List<string> assignments, List<int> users, List<bool> tasksChk, List<bool> assignmentsChk)
        {
            if(tasks != null)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    var taskObj = new MeetingTasks();
                    taskObj.Description = tasks[i];
                    taskObj.MeetingId = meetingId;

                    if (tasksChk != null)
                    {
                        taskObj.IsDone = tasksChk[i];
                    }

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

                    if(assignmentsChk != null)
                    {
                        assignmentObj.IsDone = assignmentsChk[i];
                    }

                    _db.MeetingAssignments.Add(assignmentObj);

                    //MailSender.SendMail(GetUserById(assignmentObj.AssignedToUserId), "קיבלת משימה חדשה", assignmentObj.Description);
                }
            }

            _db.SaveChanges();
        }

        public Users GetUserById(int userId)
        {
            return _db.Users.Find(userId);
        }

        public MeetingData GetMeetingById(int id)
        {
            return _db.MeetingData.Find(id);
        }

        public void UpdateMeetingData(string creationDate, string meetingSubject, int meetingId, string meetingSummary, List<string> tasks, List<string> assignments, List<int> users, List<bool> tasksChk, List<bool> assignmentsChk)
        {
            var meetingData = GetMeetingById(meetingId);
            meetingData.UpdateDate = DateTime.Now;
            meetingData.MeetingSummary = meetingSummary;
            meetingData.CreationDate = DateTime.ParseExact(creationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
            meetingData.MeetingSubject = meetingSubject;

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

            UpdateData(meetingId, tasks, assignments, users, tasksChk, assignmentsChk);
        }

        public void DeleteMeetingData(int meetingId)
        {
            var meetingData = GetMeetingById(meetingId);
            meetingData.UpdateDate = DateTime.Now;
            meetingData.IsArchived = true;

            _db.SaveChanges();
        }
    }
}