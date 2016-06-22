using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;
using Md = ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class GroupDataService :DataServiceBase, IGroupDataService
    {
         public GroupDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public IEnumerable<Group> GetAllGroups()
         {
             return this.UnitOfWork.Get<Group>().AsQueryable()
                    .Where(y => y.DeletedOn == null);
         }

         public IEnumerable<Group> GetGroupsForSpecifiedYearsAndCourses(Md.YearCourseGroup input)
         {
             int yearId, courseId;
             var groups = new List<Group>();
             for (int i = 0; i < input.Years.Count; i++)
             {
                 yearId = input.Years[i].YearId;
                 for (int j = 0; j < input.Courses.Count; j++)
                 {
                     courseId = input.Courses[j].CourseId;
                     var group = this.UnitOfWork.Get<Group>().AsQueryable().Where(y => y.DeletedOn == null && (y.YearId == yearId && y.CourseId == courseId));
                     if (group != null)
                     {
                         foreach (var g in group) {
                             groups.Add(g);
                         }
                         
                     }
                 }
             }

             return groups;
         }

         public Group GetGroup(int groupId)
         {
             return this.UnitOfWork.Get<Group>().AsQueryable()
                .FirstOrDefault(c => c.GroupId == groupId);
         }
    }
}
