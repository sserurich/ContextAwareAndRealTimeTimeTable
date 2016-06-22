using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public interface IActivityDataService
    {
       IEnumerable<Activity> GetAllActivities();


       IEnumerable<Activity> GetActivity(int activityId);

       int SaveActivity(Models.Activity activity);
    }
}
