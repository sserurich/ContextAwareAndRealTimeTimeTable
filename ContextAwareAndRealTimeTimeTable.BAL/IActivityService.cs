using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface IActivityService
    {
        IEnumerable<Activity> GetAllActivities();
        IEnumerable<Activity> GetAllTodaysActivities();
        IEnumerable<Activity> GetOnGoingTimeSchedules();
        ListOfOngoingActivities GetAllOnGoingTimetableSchedules();

        Activity GetActivity(int activityId);

        int SaveActivity(Activity activity);

        IEnumerable<Activity> GetAllActivitiesOfCertainType(string type);

        List<Activity> GetPreferedActivities(StudentPreferences studentPreferences);

        IEnumerable<Activity> GetAllTomorrowsActivities();
    }
}
