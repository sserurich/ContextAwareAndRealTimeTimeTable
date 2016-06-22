using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;
using ContextAwareAndRealTimeTimeTable.DAL;
using AutoMapper;
using ContextAwareAndRealTimeTimeTable.Helpers;
using System.Threading;
using System.Configuration;
using HL = ContextAwareAndRealTimeTimeTable.Helpers;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public class ActivityService: IActivityService
    {
        private static string senderId = ConfigurationManager.AppSettings["SenderId"];
        private static string apiKey = ConfigurationManager.AppSettings["ApiKey"];
        private static string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
        private string baseUrl = "http://api.kayesms.com/api/v1/sms/send/?apiKey=" + apiKey;

        List<Activity> onGoingActivities = new List<Activity>();
        private IActivityDataService _dataService;
        private IGroupService _groupService;
        private IStudentSubjectService _studentSubjectService;
        private IStudentGroupService _studentGroupService;
        private IStudentService _studentService;

        public ActivityService( ActivityDataService dataService, GroupService groupService, StudentSubjectService studentSubjectService, StudentGroupService studentGroupService, StudentService studentService)
        {
            this._dataService = dataService;
            this._groupService = groupService;
            this._studentSubjectService = studentSubjectService;
            this._studentGroupService = studentGroupService;
            this._studentService = studentService;
        }

        public Activity GetActivity(int activityId)
        {
            var result = this._dataService.GetActivity(activityId);
            var activity = MapEfActivityObjectsToModelsActivityObject(result.ToList());          
            return activity.FirstOrDefault();
        }

        public int SaveActivity(Activity activity)
        {
            int activityId = this._dataService.SaveActivity(activity);
            NotifyUsersWhenActivityIsModified(activityId);
            return activityId;

        }

        public void NotifyUsersWhenActivityIsModified(int activityId)
        {
            var activity = GetActivity(activityId);
            if (activity != null) { 
            List<string> phoneNumbersToNotify = new List<string>();
            string messageToSend = "Time Table Activity " + activity.Subject.Name +" "+ " has been modified, check online for the changes";
            var studentIdsAssociatedToAnActivity = GetUsersAssociatedToAnActivity(activity);
            if(studentIdsAssociatedToAnActivity != null){
                var userIdsToBeNotified = _studentService.GetUserIds(studentIdsAssociatedToAnActivity);
                if (userIdsToBeNotified != null)
                {
                    var usersToBeNotified = _studentService.GetAllUsersWithTheSpecifiedUserIds(userIdsToBeNotified);
                    foreach (var user in usersToBeNotified)
                    {
                        string msgBody = "Hi " + user.FirstName + "," + messageToSend;
                        HL.SmsHelper.SendSmsToOneContact(baseUrl, user.Mobile, msgBody, senderId);
                        HL.Email mail = new HL.Email();
                        mail.MailBodyText = msgBody;
                        mail.MailBodyHtml = msgBody;
                        mail.MailToAddress = user.Email;
                        mail.MailFromAddress = fromAddress;
                        mail.Subject = activity.Subject.Name + "Has Been Modified. Check Online For Changes";
                        mail.SendMail();
                    }
                    
                }
            }
           }
             
        }

        public List<int> GetUsersAssociatedToAnActivity(Activity activity)
        {
            List<int> completeListOfUsers = new List<int>();
            var studentIds = _studentGroupService.GetAllStudentIdsAttachedToAGroup(activity.GroupId);
            var studentidList = _studentSubjectService.GetAllStudentIdsAttachedToASubject(activity.SubjectId);
            foreach (int id in studentIds)
            {
                if (studentidList.Contains(id))
                {
                    completeListOfUsers.Add(id);
                }
            }
            return completeListOfUsers;
        }

        public IEnumerable<Activity> GetAllActivitiesForTheSpecifiedGroup(int groupId)
        {
            var results = GetAllActivities();
            var groupActivities = results.AsQueryable().Where(a => a.GroupId== groupId);
            return groupActivities;
        }

        public IEnumerable<Activity> GetAllActivitiesForThePreferedSubjects(List<Subject> subjects,List<Activity> activitiesToFilter)
        {
            List<Activity> subjectActivities = new List<Activity>();
            if (activitiesToFilter.Count > 0)
            {
                if(subjects.Count> 0){
                    foreach(Subject x in subjects){                       
                        var foundsubjectActivities = activitiesToFilter.AsQueryable().
                                Where(a => a.SubjectId == x.SubjectId).GroupBy(a => a.ActivityId).Select(z => z.FirstOrDefault());
                        if (foundsubjectActivities != null)
                        {
                            foreach (Activity a in foundsubjectActivities)
                            {
                                subjectActivities.Add(a);
                            }
                        }
                    }
                }
              
            }

            return subjectActivities;
        }

        public List<Activity> GetPreferedActivities(StudentPreferences studentPreferences)
        {
            List<Activity> preferedActivities = new List<Activity>();
            List<Activity> groupActivities = new List<Activity>();
            if (studentPreferences != null)
            {
                if (studentPreferences.SelectedGroups.Count > 0)
                {
                    foreach (Group x in studentPreferences.SelectedGroups)
                    {
                       var activities = GetAllActivitiesForTheSpecifiedGroup(x.GroupId);
                       if (activities != null)
                       {
                           groupActivities = activities.ToList();
                           var activitySubjectsForEachGroup = GetAllActivitiesForThePreferedSubjects(studentPreferences.SelectedSubjects, groupActivities.ToList());
                           if (activitySubjectsForEachGroup != null)
                           {
                               foreach (Activity y in activitySubjectsForEachGroup)
                               {
                                   preferedActivities.Add(y);
                               }
                           }
                       }
                    }
                }
            }
            return preferedActivities;
        }

        public IEnumerable<Activity> GetAllActivitiesOfCertainType(string type)
        {
            var results = GetAllActivities();           
            var resultsOfCertainActivitiesType = results.AsQueryable().Where(a => a.Type == type);
            return resultsOfCertainActivitiesType;
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            var results = this._dataService.GetAllActivities();
            var activityList = MapEfActivityObjectsToModelsActivityObject(results.ToList());
            return activityList;
        }

        public IEnumerable<Activity> GetAllTodaysActivities()
        {
            var results = GetAllActivitiesOfCertainType("L");
            var todaysActivities = results.AsQueryable().Where(a => a.Day.Name == DateTime.Now.DayOfWeek.ToString());
            return todaysActivities;
        }

        public IEnumerable<Activity> GetAllTomorrowsActivities()
        {
           DateTime tomorrow = DateTime.Now.AddDays(1);

            var results = GetAllActivitiesOfCertainType("L");
            var todaysActivities = results.AsQueryable().Where(a => a.Day.Name == tomorrow.DayOfWeek.ToString());
            return todaysActivities;
        }

        public Group GetAnActivitiyYearAndCourseUsingItsGroup(int groupId)
        {
           return _groupService.GetGroup(groupId);
        }

        public ListOfOngoingActivities GetAllOnGoingTimetableSchedules()
        {
            ListOfOngoingActivities onGoingTimetableSchedules= new ListOfOngoingActivities();
            onGoingTimetableSchedules.OnGoingBramYearOneActivity = GetOnGoingTimeSchedulesInBramYearOne();
            onGoingTimetableSchedules.OnGoingBisYearOneActivity = GetOnGoingTimeSchedulesInInformationSystemsYearOne();
            onGoingTimetableSchedules.OnGoingBitYearOneActivity = GetOnGoingTimeSchedulesInBITYearOne();
            onGoingTimetableSchedules.OnGoingBlisYearOneActivity = GetOnGoingTimeSchedulesInBlisYearOne();
            onGoingTimetableSchedules.OnGoingDramYearOneActivity = GetOnGoingTimeSchedulesInDramYearOne();
            onGoingTimetableSchedules.OnGoingSEYearOneActivity = GetOnGoingTimeSchedulesInSoftwareEngineeringYearOne();
            onGoingTimetableSchedules.OnGoingCsYearOneActivity = GetOnGoingTimeSchedulesInComputerScienceYearOne();

            /**---year two --**/
            onGoingTimetableSchedules.OnGoingBramYearTwoActivity = GetOnGoingTimeSchedulesInBramYearTwo();
            onGoingTimetableSchedules.OnGoingBisYearTwoActivity = GetOnGoingTimeSchedulesInInformationSystemsYearTwo();
            onGoingTimetableSchedules.OnGoingBitYearTwoActivity = GetOnGoingTimeSchedulesInBITYearTwo();
            onGoingTimetableSchedules.OnGoingBlisYearTwoActivity = GetOnGoingTimeSchedulesInBlisYearTwo();
            onGoingTimetableSchedules.OnGoingDramYearTwoActivity = GetOnGoingTimeSchedulesInDramYearTwo();
            onGoingTimetableSchedules.OnGoingSEYearTwoActivity = GetOnGoingTimeSchedulesInSoftwareEngineeringYearTwo();
            onGoingTimetableSchedules.OnGoingCsYearTwoActivity = GetOnGoingTimeSchedulesInComputerScienceYearTwo();


            /**---year three --**/
            onGoingTimetableSchedules.OnGoingBramYearThreeActivity = GetOnGoingTimeSchedulesInBramYearThree();
            onGoingTimetableSchedules.OnGoingBisYearThreeActivity = GetOnGoingTimeSchedulesInInformationSystemsYearThree();
            onGoingTimetableSchedules.OnGoingBitYearThreeActivity = GetOnGoingTimeSchedulesInBITYearThree();
            onGoingTimetableSchedules.OnGoingBlisYearThreeActivity = GetOnGoingTimeSchedulesInBlisYearThree();
            onGoingTimetableSchedules.OnGoingDramYearThreeActivity = GetOnGoingTimeSchedulesInDramYearThree();
            onGoingTimetableSchedules.OnGoingSEYearThreeActivity = GetOnGoingTimeSchedulesInSoftwareEngineeringYearThree();
            onGoingTimetableSchedules.OnGoingCsYearThreeActivity = GetOnGoingTimeSchedulesInComputerScienceYearThree();

            /**--year four --**/
            onGoingTimetableSchedules.OnGoingSEYearFourActivity = GetOnGoingTimeSchedulesInSoftwareEngineeringYearFour();
            
            return onGoingTimetableSchedules;
        }

        public Activity GetOnGoingTimeSchedulesInComputerScienceYearOne()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearOne();
            var activity = onGoingActivities.AsQueryable().Where(a=>a.Group.CourseId == 1).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInBITYearOne()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearOne();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 2).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInSoftwareEngineeringYearOne()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearOne();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 4).FirstOrDefault();
            return activity;
        }

        
        public Activity GetOnGoingTimeSchedulesInBlisYearOne()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearOne();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 6).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInBramYearOne()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearOne();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 7).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInInformationSystemsYearOne()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearOne();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 8).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInDramYearOne()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearOne();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 9).FirstOrDefault();
            return activity;
        }

        public IEnumerable<Activity> GetOnGoingTimeSchedulesInYearOne()
        {
            var onGoingActivitiesInYearOne = GetOnGoingTimeSchedules();
            var activities = onGoingActivities.AsQueryable().Where(a => a.Group.YearId == 1);
            return activities;
        }

        /*---------ongoing timeschedules in year 2------*/

        public Activity GetOnGoingTimeSchedulesInComputerScienceYearTwo()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearTwo();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 1).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInBITYearTwo()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearTwo();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 2).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInSoftwareEngineeringYearTwo()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearTwo();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 4).FirstOrDefault();
            return activity;
        }
        
        public Activity GetOnGoingTimeSchedulesInBlisYearTwo()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearTwo();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 6).FirstOrDefault();
            return activity;
        }
        
        public Activity GetOnGoingTimeSchedulesInBramYearTwo()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearTwo();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 7).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInInformationSystemsYearTwo()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearTwo();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 8).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInDramYearTwo()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearTwo();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 9).FirstOrDefault();
            return activity;
        }

        public IEnumerable<Activity> GetOnGoingTimeSchedulesInYearTwo()
        {
            var onGoingActivitiesInYearOne = GetOnGoingTimeSchedules();
            var activities = onGoingActivities.AsQueryable().Where(a => a.Group.YearId == 2);
            return activities;
        }

        public Activity GetOnGoingTimeSchedulesInComputerScienceYearThree()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearThree();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 1).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInBITYearThree()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearThree();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 2).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInSoftwareEngineeringYearThree()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearThree();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 4).FirstOrDefault();
            return activity;
        }
        
        public Activity GetOnGoingTimeSchedulesInBlisYearThree()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearThree();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 6).FirstOrDefault();
            return activity;
        }
        
        public Activity GetOnGoingTimeSchedulesInBramYearThree()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearThree();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 7).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInInformationSystemsYearThree()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearThree();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 8).FirstOrDefault();
            return activity;
        }

        public Activity GetOnGoingTimeSchedulesInDramYearThree()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearThree();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 9).FirstOrDefault();
            return activity;
        }

        public IEnumerable<Activity> GetOnGoingTimeSchedulesInYearThree()
        {
            var onGoingActivitiesInYearOne = GetOnGoingTimeSchedules();
            var activities = onGoingActivities.AsQueryable().Where(a => a.Group.YearId == 3);
            return activities;
        }

        public Activity GetOnGoingTimeSchedulesInSoftwareEngineeringYearFour()
        {
            var onGoingActivities = GetOnGoingTimeSchedulesInYearFour();
            var activity = onGoingActivities.AsQueryable().Where(a => a.Group.CourseId == 4).FirstOrDefault();
            return activity;
        }

        public IEnumerable<Activity> GetOnGoingTimeSchedulesInYearFour()
        {
            var onGoingActivitiesInYearOne = GetOnGoingTimeSchedules();
            var activities = onGoingActivities.AsQueryable().Where(a => a.Group.YearId == 4);
            return activities;
        }
        
        public IEnumerable<Activity> GetOnGoingTimeSchedules()
        {
            DateTime currentTime = DateTime.Now.AddHours(2);
            var todaysActivities = GetAllTodaysActivities();
            foreach (Activity x in todaysActivities)
            {
                DateTime activityStartDateTime = Convert.ToDateTime(DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year + " " + x.StartTime.Trim());
              //  DateTime activityStartDateTime = activityStartDateTimes.AddHours(2);
                DateTime activityEndDateTime = Convert.ToDateTime(DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year + " " + x.EndTime.Trim());
              //  DateTime activityEndDateTime = activityEndDateTimes.AddHours(2);
                if (activityStartDateTime <= currentTime && currentTime <= activityEndDateTime)
                {
                    var dateSuffix = "AM";
                    var pmDate = Convert.ToDateTime(DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year + " " + "12:00");
                    if (activityStartDateTime >= pmDate)
                    {
                        dateSuffix = "PM";
                    }

                    if (activityEndDateTime >= pmDate)
                    {
                        dateSuffix = "PM";
                    }
                    x.StartTime = x.StartTime + dateSuffix;
                    x.EndTime = x.EndTime + dateSuffix;
                    onGoingActivities.Add(x);
                }
            }      
            return onGoingActivities;
        }
                
       

        public List<Activity> EmptyAList(List<Activity> list)
        {
            foreach (var x in list)
            {
                list.Remove(x);
            }
            return list;
        }

        public IEnumerable<Activity> MapEfActivityObjectsToModelsActivityObject(List<EF.Models.Activity> activityList)
        {
            var modelsActivityList = new List<Activity>();
            foreach (EF.Models.Activity activity in activityList)
            {
                var activitySubject =ActivityHelper.CreateActivitySubject(activity.Subject);
                var activityRoom = ActivityHelper.CreateActivityRoom(activity.Room);
                var activityLecturer = ActivityHelper.CreateActivityLecturer(activity.Lecturer);
                var activityDay = ActivityHelper.CreateActivityDay(activity.Day);
                var activityGroup = ActivityHelper.CreateActivityGroup(activity.Group);
                var activityComments = ActivityHelper.CreateActivityComments(activity.Comments);

                modelsActivityList.Add(new ContextAwareAndRealTimeTimeTable.Models.Activity()
                {
                   ActivityId = activity.ActivityId,
                   StartTime= activity.StartTime,
                   EndTime = activity.EndTime,
                   Room = activityRoom,
                   Day= activityDay,
                   Lecturer = activityLecturer,
                   Comments = activityComments,
                   Group = activityGroup,
                   Subject = activitySubject,
                   RoomId = activity.RoomId,
                   SubjectId = activity.SubjectId,
                   LecturerId =activity.LecturerId,
                   GroupId = activity.GroupId,
                   DayId= activity.DayId,
                   Type = activity.Type

                });
            }
            return modelsActivityList;
        }
    }
}
