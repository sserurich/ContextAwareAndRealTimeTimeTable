using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContextAwareAndRealTimeTimeTable.Models;
using ContextAwareAndRealTimeTimeTable.BAL;

namespace ContextAwareAndRealTimeTimeTable.Web.Controllers
{
    public class GlobalApiController : ApiController
    {
        private IYearService _yearService;
        private IDayService _dayService;
        private IRoomService _roomService;
        private ILecturerService _lecturerService;
        private ISubjectService _subjectService;
        private IGroupService _groupService;
        private IStudentService _studentService;
        private IActivityService _activityService;
        private IStudentSubjectService _studentSubjectService;
        private IStudentGroupService _studentGroupService;
        private ICourseService _courseService;
        private IStudentPreferenceService _studentPreferenceService;
        private IStudentCourseService _studentCourseService;
        private IStudentYearService _studentYearService;
        private ICommentService _commentService;
      

        public GlobalApiController()
        {

        }

        public GlobalApiController(IYearService yearService,IRoomService roomService, ILecturerService lecturerService, IDayService dayService,
            IGroupService groupService, IStudentService studentService, ISubjectService subjectService, ICourseService courseService,
            IActivityService activityService, IStudentGroupService studentGroupService,IStudentSubjectService studentSubjectService,
            IStudentPreferenceService studentPreferenceService,IStudentYearService studentYearService, IStudentCourseService studentCourseService,
            ICommentService commentService)
        {
            this._yearService = yearService;
            this._dayService = dayService;
            this._roomService = roomService;
            this._lecturerService = lecturerService;
            this._groupService = groupService;
            this._studentService = studentService;
            this._subjectService = subjectService;
            this._studentSubjectService = studentSubjectService;
            this._activityService = activityService;
            this._studentGroupService = studentGroupService;
            this._courseService = courseService;
            this._studentPreferenceService = studentPreferenceService;
            this._studentCourseService = studentCourseService;
            this._studentYearService = studentYearService;
            this._commentService = commentService;
        }

        [HttpGet]
        [ActionName("GetAllYears")]
        public IEnumerable<Year> GetAllYears()
        {
            return _yearService.GetAllYears();
        }

        [HttpGet]
        [ActionName("GetAllActivities")]
        public IEnumerable<Activity> GetAllActivities()
        {
            return _activityService.GetAllActivities();
        }

        [HttpGet]
        [ActionName("GetAllExamsActivities")]
        public IEnumerable<Activity> GetAllExamsActivities()
        {
            return _activityService.GetAllActivitiesOfCertainType("E");
        }

        [HttpGet]
        [ActionName("GetAllTestActivities")]
        public IEnumerable<Activity> GetAllTestActivities()
        {
            return _activityService.GetAllActivitiesOfCertainType("T");
        }

        [HttpGet]
        [ActionName("GetAllOnGoingTimetableSchedules")]
        public ListOfOngoingActivities GetAllOnGoingTimetableSchedules()
        {
            return _activityService.GetAllOnGoingTimetableSchedules();
        }
        [HttpGet]
        [ActionName("GetAllTomorrowsActivities")]
        public IEnumerable<Activity> GetAllTomorrowsActivities()
        {
            return _activityService.GetAllTomorrowsActivities();
        }

        [HttpGet]
        [ActionName("GetAllTodaysActivities")]
        public IEnumerable<Activity> GetAllTodaysActivities()
        {
            return _activityService.GetAllTodaysActivities();
        }

        [HttpGet]
        [ActionName("GetActivity")]
        public Activity GetActivity(int activityId)
        {
            return _activityService.GetActivity(activityId);
        }

        [HttpPost]
        [ActionName("SaveActivity")]
        public int SaveMessage(Activity activity)
        {
            return _activityService.SaveActivity(activity);
        }

        [HttpGet]
        [ActionName("GetAllOnGoingActivities")]
        public IEnumerable<Activity> GetAllOnGoingActivities()
        {
            return _activityService.GetOnGoingTimeSchedules();
        }
        [ActionName("GetAllSubjects")]
        public IEnumerable<Subject> GetAllSubjects()
        {
            return _subjectService.GetAllSubjects();
        }

        [ActionName("GetAllRooms")]
        public IEnumerable<Room> GetAllRooms()
        {
            return _roomService.GetAllRooms();
        }

        [ActionName("GetAllDays")]
        public IEnumerable<Day> GetAllDays()
        {
            return _dayService.GetAllDays();
        }

        [ActionName("GetAllGroups")]
        public IEnumerable<Group> GetAllGroups()
        {
            return _groupService.GetAllGroups();
        }


        [ActionName("GetAllLecturers")]
        public IEnumerable<Lecturer> GetAllLecturers()
        {
            return _lecturerService.GetAllLecturers();
        }

        [HttpGet]
        [ActionName("GetAllCourses")]
        public IEnumerable<Course> GetAllCourses()
        {
            return _courseService.GetAllCourses();
        }

        [HttpPost]
        [ActionName("GetGroupsForSpecifiedYearsAndCourses")]
        public IEnumerable<Group> GetGroupsForSpecifiedYearsAndCourses(YearCourseGroup input)
        {
            return _groupService.GetGroupsForSpecifiedYearsAndCourses(input);
        }

        [HttpPost]
        [ActionName("GetSubjectsForSpecifiedCoursesYearsAndGroups")]
        public IEnumerable<Subject> GetSubjectsForSpecifiedCoursesYearsAndGroups(CourseGroups input)
        {
            return _subjectService.GetSubjectsForSpecifiedCoursesYearsAndGroups(input);
        }

        [HttpPost]
        [ActionName("SaveStudentPreferences")]
        public bool SaveStudentPreferences(StudentPreferences studentPreferences)
        {
            bool saved = false;
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            int studentId = _studentService.GetStudentId(userId);
            if (studentId > 0)
            {
                studentPreferences.studentId = studentId;
             saved =  _studentPreferenceService.SaveStudentPreferences(studentPreferences);
            }
            return saved;
           
        }

        [HttpPost]
        [ActionName("SaveStudent")]
        public int GetGroupsForSpecifiedYearsAndCourses(Student student)
        {
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            student.UserId = userId;
            return _studentService.SaveStudent(student);
        }

        //tobe called by mobile application

        [HttpPost]
        [ActionName("GetStudentId")]
        public int GetStudentId(Student student)
        {
            return _studentService.GetStudentsId(student);
        }

        [HttpGet]
        [ActionName("GetStudentPreferences")]
        public StudentPreferences GetStudentPreferences()
        {
            StudentPreferences studentPreferences = new StudentPreferences();
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            int studentId = _studentService.GetStudentId(userId);
            if (studentId > 0)
            {
              studentPreferences=_studentPreferenceService.GetStudentPreferences(studentId);
            }
            return studentPreferences;
        }

        [HttpGet]
        [ActionName("GetPreferedActivities")]
        public List<Activity> GetPreferedActivities()
        {
            StudentPreferences studentPreferences = new StudentPreferences();
            List<Activity> preferedActivities = new List<Activity>();
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            int studentId = _studentService.GetStudentId(userId);
            if (studentId > 0)
            {
                studentPreferences = _studentPreferenceService.GetStudentPreferences(studentId);
                preferedActivities = _activityService.GetPreferedActivities(studentPreferences);
            }
           
            return preferedActivities;
        }


        [HttpGet]
        [System.Web.Http.ActionName("GetAllActivityComments")]
        public IEnumerable<Comment> GetAllMessageComments(int activityId)
        {
            return _commentService.GetAllActivityComments(activityId);
        }

        [HttpGet]
        [ActionName("GetComment")]
        public Comment GetComment(int commentId, int activityId)
        {
            return _commentService.GetComment(commentId, activityId);
        }

        [HttpPost]
        [ActionName("SaveComment")]
        public int SaveComment(Comment comment)
        {
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            comment.CreatedBy = userId;
            return _commentService.SaveComment(comment);
        }

        [HttpGet]
        [ActionName("DeleteComment")]
        public void DeleteComment(int commentId, int activityId)
        {
            _commentService.DeleteComment(commentId, activityId);
        }

    }
}
