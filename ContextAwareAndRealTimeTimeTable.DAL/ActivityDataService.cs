using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class ActivityDataService:DataServiceBase, IActivityDataService
    {
         public ActivityDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public IEnumerable<Activity> GetAllActivities()
         {
             return this.UnitOfWork.Get<Activity>().AsQueryable()
                    .Where(y => y.DeletedOn == null);
         }

         public IEnumerable<Activity> GetActivity(int activityId)
         {
             return this.UnitOfWork.Get<Activity>().AsQueryable()
                   .Where(y => y.DeletedOn == null && y.ActivityId == activityId);
         }

         public int SaveActivity(Models.Activity activity)
         {
             var c = this.UnitOfWork.Get<Activity>().AsQueryable()
                       .FirstOrDefault(d => d.ActivityId == activity.ActivityId);
             if (c != null)
             {
                 c.StartTime = activity.StartTime;
                 c.EndTime = activity.EndTime;
                 c.GroupId = activity.GroupId;
                 c.LecturerId = activity.LecturerId;
                 c.RoomId = activity.RoomId;
                 c.DayId = activity.DayId;
                 c.SubjectId = activity.SubjectId;                 
                 c.UpdatedOn = DateTime.Now;
                 this.UnitOfWork.Get<Activity>().Update(c);
                 this.UnitOfWork.SaveChanges();
             }
             return activity.ActivityId;
         }

    }
}
